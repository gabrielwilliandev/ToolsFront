using FluentValidation;
using System.Xml.Linq;
using Tools.Application.Common.Result;
using Tools.Application.DTOs.Tools;
using Tools.Application.Interfaces;
using Tools.Application.Notifications;
using Tools.Domain.Entities;
using Tools.Domain.Exceptions;

namespace Tools.Application.Services
{
    public class ToolService : IToolService
    {
        private readonly IToolRepository _toolRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IValidator<CreateToolRequest> _createValidator;
        private readonly IValidator<UpdateToolRequest> _updateValidator;
        private readonly NotificationContext _notificationContext;
        private readonly IListaRepository _listaRepository;

        public ToolService(
            IToolRepository toolRepository,
            ITagRepository tagRepository,
            IValidator<CreateToolRequest> createValidator,
            IValidator<UpdateToolRequest> updateValidator, NotificationContext notificationContext, IListaRepository listaRepository)
        {
            _toolRepository = toolRepository;
            _tagRepository = tagRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _notificationContext = notificationContext;
            _listaRepository = listaRepository;
        }

        public async Task<ToolResponse?> CreateToolAsync(CreateToolRequest request, Guid userId)
        {
            var validation = _createValidator.Validate(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => new Error($"validation.{e.PropertyName}", e.ErrorMessage));
                _notificationContext.AddError(errors);
                return null;
            }

            var lista = await _listaRepository.GetByIdAsync(userId, request.ListaId);
            if (lista == null || lista.UserId != userId)
            {
                _notificationContext.AddErrors("lista.notFound", "Lista não encontrada ou acesso negado.");
                return null;
            }

            try
            {
                var tool = new Tool(request.Name, request.Description, request.ListaId);
                var tags = request.Tags?
                                    .Where(t => !string.IsNullOrWhiteSpace(t))
                                    .Select(t => t.Trim().ToLower())
                                    .Distinct()
                                    ?? Enumerable.Empty<string>();

                var tagNames = tags.Select(t => t.Trim().ToLower()).ToList();

                var existingTags = await _tagRepository.GetTagByNameAsync(tagNames);

                foreach (var name in tagNames)
                {
                    var tag = existingTags.FirstOrDefault(t => t.Name == name)
                              ?? new Tag(name);

                    tool.Tags.Add(tag);
                }

                await _toolRepository.AddToolAsync(tool);
                await _toolRepository.SaveChangesAsync();

                return MapToToolResponse(tool);
            }
            catch (DomainException ex)
            {
                _notificationContext.AddErrors(ex.Code, ex.Message);
                return null;
            }
        }
            
        

        public async Task<List<ToolResponse>> GetAllToolsAsync(Guid userId)
        {
            var tools = await _toolRepository.GetAllAsync(userId);
            return tools.Select(MapToToolResponse).ToList();
            
        }

        public async Task<ToolResponse?> GetToolByIdAsync(Guid id, Guid userId)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id, userId);

            if (tool == null)
            {
                _notificationContext.AddErrors("tool.notFound", "Ferramenta não encontrada!");
                return null;
            }

            return MapToToolResponse(tool);
        }

        public async Task<IEnumerable<ToolResponse>> SearchToolsAsync(string query, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                _notificationContext.AddErrors("search.invalidQuery", "A consulta de pesquisa não pode ser vazia.");
                return Enumerable.Empty<ToolResponse>();
            }

            var tools = await _toolRepository.SearchAsync(query, userId);
            return tools.Select(MapToToolResponse);
        }

        public async Task<bool> UpdateToolAsync(Guid id, UpdateToolRequest request, Guid userId)
        {
            var validation = _updateValidator.Validate(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => new Error($"validation.{e.PropertyName}", e.ErrorMessage));
                _notificationContext.AddError(errors);
                return false;
            }

            var tool = await _toolRepository.GetToolByIdAsync(id, userId);
            if (tool == null)
            {
                _notificationContext.AddErrors("tool.notFound", "Ferramenta não encontrada.");
                return false;
            }

            try
            {
                tool.Update(request.Name, request.Description);

                var newTagNames = request.Tags?
                                    .Where(t => !string.IsNullOrWhiteSpace(t))
                                    .Select(t => t.Trim().ToLower())
                                    .Distinct()
                                    .ToList() ?? new List<string>();

                var tagsToRemove = tool.Tags.Where(t => !newTagNames.Contains(t.Name.ToLower())).ToList();
                foreach (var tag in tagsToRemove)
                {
                    tool.Tags.Remove(tag);
                }

                var currentTagNames = tool.Tags.Select(t => t.Name.ToLower()).ToList();
                var tagsToAddNames = newTagNames.Where(name => !currentTagNames.Contains(name)).ToList();

                if (tagsToAddNames.Any())
                {
                    var existingTags = await _tagRepository.GetTagByNameAsync(tagsToAddNames) ?? new List<Tag>();

                    foreach (var tagName in tagsToAddNames)
                    {
                        var tagToAdd = existingTags.FirstOrDefault(t => t.Name == tagName)
                                       ?? new Tag(tagName);

                        tool.Tags.Add(tagToAdd);
                    }
                }

                await _toolRepository.SaveChangesAsync();
                return true;
            }
            catch (DomainException ex)
            {
                _notificationContext.AddErrors(ex.Code, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                _notificationContext.AddErrors("database.error", $"Erro: {message}");
                return false;
            }
        }
        public async Task<bool> DeleteToolAsync(Guid id, Guid userId)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id, userId);
            if (tool == null)
            {
                _notificationContext.AddErrors("tool.notFound", "Ferramenta não encontrada.1");
                return false;
            }

            _toolRepository.RemoveTool(tool);
            await _toolRepository.SaveChangesAsync();

            return true;
        }

        private static ToolResponse MapToToolResponse(Tool tool)
        {
            return new ToolResponse
            {
                Id = tool.Id,
                ListaId = tool.ListaId,
                ListaNome = tool.Lista?.Name ?? string.Empty,
                Name = tool.Name,
                Description = tool.Description,
                Tags = tool.Tags.Select(tag => tag.Name).ToList()
            };
        }
    }
}
