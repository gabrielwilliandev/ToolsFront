using System.Xml.Linq;
using Tools.Application.Common.Result;
using Tools.Application.DTOs.Tools;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;

namespace Tools.Application.Services
{
    public class ToolService : IToolService
    {
        private readonly IToolRepository _toolRepository;
        private readonly ITagRepository _tagRepository;
        public ToolService(IToolRepository toolRepository, ITagRepository tagRepository)
        {
            _toolRepository = toolRepository;
            _tagRepository = tagRepository;
        }
        public async Task<Result<ToolResponse>> CreateToolAsync(CreateToolRequest request)
        {
            var tool = new Tool(request.Name, request.Description);
            
            var normalizedTags = request.Tags
                .Where(tag => !string.IsNullOrWhiteSpace(tag))
                .Select(tag => tag.Trim().ToLower())
                .Distinct()
                .ToList() ?? new();

            foreach (var tagName in normalizedTags)
            {
                var existingTag = await _tagRepository.GetTagByNameAsync(tagName);

                if(existingTag != null)
                {
                    tool.Tags.Add(existingTag);
                }
                else
                {
                    tool.Tags.Add(new Tag(tagName));
                }
            }
            await _toolRepository.AddToolAsync(tool);
            await _toolRepository.SaveChangesAsync();

            var response = MapToToolResponse(tool);

            return Result<ToolResponse>.Success(response);
        }

        public async Task <Result<List<ToolResponse>>> GetAllToolsAsync()
        {
            var tools = await _toolRepository.GetAllAsync();
            var response = tools.Select(MapToToolResponse).ToList();
            return Result<List<ToolResponse>>.Success(response);
        }

        public async Task<Result<ToolResponse>> GetToolByIdAsync(Guid id)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);

            if (tool == null)
            {
                return Result<ToolResponse>.Failure(new List<Error>
                    { new Error("tool.notFound", "Ferramenta não encontrada") });
            }

            return Result<ToolResponse>.Success(MapToToolResponse(tool));
        }

        public async Task<Result<IEnumerable<ToolResponse>>> SearchToolsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Result<IEnumerable<ToolResponse>>.Failure(new List<Error>
                    { new Error("search.invalidQuery", "A consulta de pesquisa não pode ser vazia") });
            }

            var tools = await _toolRepository.SearchAsync(query);
            var response = tools.Select(MapToToolResponse).ToList();
            return Result<IEnumerable<ToolResponse>>.Success(response);
        }

        public async Task<Result<bool>> UpdateToolAsync(Guid id, UpdateToolRequest request)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
            {
                return Result<bool>.Failure(new List<Error>
                    { new Error("tool.notFound", "Ferramenta não encontrada") });
            }

            tool.Update(request.Name, request.Description);
            tool.Tags.Clear();

            var normalizedTags = request.Tags
                .Where(tag => !string.IsNullOrWhiteSpace(tag))
                .Select(tag => tag.Trim().ToLower())
                .Distinct()
                .ToList() ?? new();

            foreach (var tagName in normalizedTags)
            {
                var existingTag = await _tagRepository.GetTagByNameAsync(tagName);
                tool.Tags.Add(existingTag ?? new Tag { Name = tagName });
            }
            await _toolRepository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        public async Task<Result<bool>> DeleteToolAsync(Guid id)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
            {
                return Result<bool>.Failure(new List<Error>
                    { new Error("tool.notFound", "Ferramenta não encontrada") });
            }

            _toolRepository.RemoveTool(tool);
            await _toolRepository.SaveChangesAsync();
            return Result<bool>.Success(true);
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
