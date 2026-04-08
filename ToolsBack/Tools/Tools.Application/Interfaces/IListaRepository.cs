using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface IListaRepository
    {
        Task CreateAsync(Lista lista);
        Task<List<Lista>> GetAllByUserAsync(Guid userId);
        Task<Lista?> GetByIdAsync(Guid id, Guid userId);
        Task UpdateAsync(Lista lista);
        Task DeleteAsync(Lista lista);
    }
}
