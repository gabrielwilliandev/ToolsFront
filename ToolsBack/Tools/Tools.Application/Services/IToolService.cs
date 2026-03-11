using Tools.Application.Common.Result;
using Tools.Application.DTOs.Tools;

namespace Tools.Application.Services
{
    public interface IToolService
    {
        Task<List<ToolResponse>> GetAllToolsAsync();
        Task<ToolResponse?> GetToolByIdAsync(Guid id);
        Task<IEnumerable<ToolResponse>> SearchToolsAsync(string query);
        Task<ToolResponse> CreateToolAsync(CreateToolRequest request);
        Task<bool> UpdateToolAsync(Guid id, UpdateToolRequest request);
        Task<bool> DeleteToolAsync(Guid id);
    }
}
