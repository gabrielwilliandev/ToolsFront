using Tools.Application.Common.Result;
using Tools.Application.DTOs.Tools;

namespace Tools.Application.Services
{
    public interface IToolService
    {
        Task<Result<List<ToolResponse>>> GetAllToolsAsync();
        Task<Result<ToolResponse?>> GetToolByIdAsync(Guid id);
        Task<Result<IEnumerable<ToolResponse>>> SearchToolsAsync(string query);
        Task<Result<ToolResponse>> CreateToolAsync(CreateToolRequest request);
        Task<Result<bool>> UpdateToolAsync(Guid id, UpdateToolRequest request);
        Task<Result<bool>> DeleteToolAsync(Guid id);
    }
}
