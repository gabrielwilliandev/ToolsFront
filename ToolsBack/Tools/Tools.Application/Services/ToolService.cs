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

        public ToolService(
            IToolRepository toolRepository,
            ITagRepository tagRepository,
            IValidator<CreateToolRequest> createValidator,
            IValidator<UpdateToolRequest> updateValidator, NotificationContext notificationContext)
        {
            _toolRepository = toolRepository;
        _tagRepository = tagRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _notificationContext = notificationContext;
        }

        public async Task<ToolResponse?> CreateToolAsync(CreateToolRequest request)
        {
            var validation = _createValidator.Validate(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => new Error($"validation.{e.PropertyName}", e.ErrorMessage));
                _notificationContext.AddError(errors);
                return null;
            }

            try
            {
                var tool = new Tool(request.Name, request.Description);
                var tags = request.Tags?.Where(t => !string.IsNullOrWhiteSpace(t)).Distinct(StringComparer.OrdinalIgnoreCase) ?? Enumerable.Empty<string>();

                foreach (var tagName in tags)
                {
                    var normalized = tagName.Trim().ToLower();
                    var existingTag = await _tagRepository.GetTagByNameAsync(normalized);
                    tool.Tags.Add(existingTag ?? new Tag(normalized));
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
            
        

        public async Task<List<ToolResponse>> GetAllToolsAsync()
        {
            var tools = await _toolRepository.GetAllAsync();
            return tools.Select(MapToToolResponse).ToList();
            
        }

        public async Task<ToolResponse?> GetToolByIdAsync(Guid id)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);

            if (tool == null)
            {
                _notificationContext.AddErrors("tool.notFound", "Ferramenta não encontrada!");
                return null;
            }

            return MapToToolResponse(tool);
        }

        public async Task<IEnumerable<ToolResponse>> SearchToolsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                _notificationContext.AddErrors("search.invalidQuery", "A consulta de pesquisa não pode ser vazia.");
                return Enumerable.Empty<ToolResponse>();
            }

            var tools = await _toolRepository.SearchAsync(query);
            return tools.Select(MapToToolResponse);
        }

        public async Task<bool> UpdateToolAsync(Guid id, UpdateToolRequest request)
        {
            var validation = _updateValidator.Validate(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => new Error($"validation.{e.PropertyName}", e.ErrorMessage));
                _notificationContext.AddError(errors);
                return false;
            }

            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
            {
                _notificationContext.AddErrors("tool.notFound", "Ferramenta não encontrada.");
                return false;
            }

            try
            {
                tool.Update(request.Name, request.Description);
            }
            catch (DomainException ex)
            {
                _notificationContext.AddErrors(ex.Code, ex.Message);
                return false;
            }

            tool.Tags.Clear();
            var tags = request.Tags?.Where(t => !string.IsNullOrWhiteSpace(t)).Distinct() ?? Enumerable.Empty<string>();
            foreach (var tagName in tags)
            {
                var normalized = tagName.Trim().ToLower();
                var existingTag = await _tagRepository.GetTagByNameAsync(normalized);
                tool.Tags.Add(existingTag ?? new Tag(normalized));
            }

            await _toolRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteToolAsync(Guid id)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
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
                Name = tool.Name,
                Description = tool.Description,
                Tags = tool.Tags.Select(tag => tag.Name).ToList()
            };
        }
    }
}
