using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface IToolRepository
    {
        Task AddToolAsync(Tool tool);
        Task<Tool?> GetToolByIdAsync(Guid id);
        Task UpdateToolAsync(Tool tool);
        Task DeleteToolAsync(Guid id);
        Task<IEnumerable<Tool>> SearchAsync(string query);
        Task<List<Tool>> GetAllAsync();

    }
}
