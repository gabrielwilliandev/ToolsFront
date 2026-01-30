using WebApplication1.Application.DTOs;

namespace WebApplication1.Application.Services
{
    public interface IToolsService
    {
        public Task<IEnumerable<ToolsResponseDto>> GetBy(string? tag);
        public Task<ToolsResponseDto> CreateTool(CreateToolDto toolDto);
        public Task DeleteTool(int id);

    }
}
