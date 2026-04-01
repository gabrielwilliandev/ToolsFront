using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface IToolRepository
    {
        Task AddToolAsync(Tool tool);
        Task<Tool?> GetToolByIdAsync(Guid id, Guid userId);
        Task<IEnumerable<Tool>> SearchAsync(string query, Guid userId);
        Task<List<Tool>> GetAllAsync(Guid userId);
        void RemoveTool(Tool tool);
        Task SaveChangesAsync();

        Task<Tool?> GetToolByIdWithTagsAsync(Guid id);

    }
}
