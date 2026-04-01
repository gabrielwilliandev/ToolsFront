using Tools.Application.Common.Result;
using Tools.Application.DTOs.Tools;

namespace Tools.Application.Interfaces
{
    public interface IToolService
    {
        Task<List<ToolResponse>> GetAllToolsAsync(Guid userId);
        Task<ToolResponse?> GetToolByIdAsync(Guid id, Guid userId);
        Task<IEnumerable<ToolResponse>> SearchToolsAsync(string query, Guid userId);
        Task<ToolResponse> CreateToolAsync(CreateToolRequest request, Guid userId);
        Task<bool> UpdateToolAsync(Guid id, UpdateToolRequest request, Guid userId);
        Task<bool> DeleteToolAsync(Guid id, Guid userId);
    }
}
