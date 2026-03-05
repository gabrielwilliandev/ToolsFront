using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface IToolRepository
    {
        Task AddToolAsync(Tool tool);
        Task<Tool?> GetToolByIdAsync(Guid id);
        Task<IEnumerable<Tool>> SearchAsync(string query);
        Task<List<Tool>> GetAllAsync();
        void RemoveTool(Tool tool);
        Task SaveChangesAsync();

    }
}
