using Tools.Application.DTOs.Tools;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;

namespace Tools.Application.UseCases.Queries
{
    public class ToolQueryService
    {
        private readonly IToolRepository _toolRepository;

        public ToolQueryService(IToolRepository toolRepository)
        {
            _toolRepository = toolRepository;
        }

        public async Task<List<ToolResponse>> GetAllAsync()
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
        public async Task<IEnumerable<ToolResponse>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<ToolResponse>();
            
            var tools = await _toolRepository.SearchAsync(query);
            return tools.Select(MapToToolResponse).ToList();
        }

        private ToolResponse MapToToolResponse(Tool tool)
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
