using System.Xml.Linq;
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
        public async Task<ToolResponse> CreateToolAsync(CreateToolRequest request)
        {
            var tool = new Tool(request.Name, request.Description);
            
            var normalizedTags = request.Tags
                .Where(tag => !string.IsNullOrWhiteSpace(tag))
                .Select(tag => tag.Trim().ToLower())
                .Distinct()
                .ToList() ?? new();

            foreach (var tagName in normalizedTags)
            {
                var existingTag = _tagRepository.GetTagByNameAsync(tagName);

                if(existingTag is not null)
                {
                    tool.Tags.Add(existingTag.Result);
                }
                else
                {
                    tool.Tags.Add(new Tag(tagName));
                }
            }
            await _toolRepository.AddToolAsync(tool);
            await _toolRepository.SaveChangesAsync();

            return MapToToolResponse(tool);
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
                return null;

            return MapToToolResponse(tool);
        }

        public async Task<IEnumerable<ToolResponse>> SearchToolsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<ToolResponse>();

            var tools = await _toolRepository.SearchAsync(query);
            return tools.Select(MapToToolResponse).ToList();
        }

        public async Task<bool> UpdateToolAsync(Guid id, UpdateToolRequest request)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
                return false;

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
            return true;
        }
        public async Task<bool> DeleteToolAsync(Guid id)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
                return false;

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
