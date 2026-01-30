using WebApplication1.Application.DTOs;
using WebApplication1.Application.Mappers;
using WebApplication1.Domain.Entities;
using WebApplication1.Infra.Repositories;

namespace WebApplication1.Application.Services
{
    public class ToolsService : IToolsService
    {
        private readonly IToolsRepository _repository;
        public ToolsService(IToolsRepository repository)
        {
            _repository = repository;
        }
        public async Task<ToolsResponseDto> CreateTool(CreateToolDto toolDto)
        {
            var tool = toolDto.MapToEntity();

            await _repository.CreateTool(tool, toolDto.Tags);
            return tool.MapToResponse();
        }

        public async Task DeleteTool(int id)
        {
            var response = await _repository.GetById(id);
            if (response == null)
                throw new Exception("Tool not found");
            await _repository.DeleteTool(id);
        }

        public async Task<IEnumerable<ToolsResponseDto>> GetBy(string? tag)
        {
            var tools = await _repository.GetAll(tag);

            return tools.Select(t => t.MapToResponse());
        }
    }
}
