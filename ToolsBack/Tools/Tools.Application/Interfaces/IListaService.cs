using Tools.Application.DTOs.Tools;

namespace Tools.Application.Interfaces
{
    public interface IListaService
    {
        Task<ListResponse?> CreateListaAsync(CreateListRequest request, Guid userId);
        Task<List<ListResponse>> GetAllListasAsync(Guid userId);
         Task<ListResponse?> GetListaByIdAsync(Guid id, Guid userId);
         Task<bool> UpdateListaAsync(Guid id, UpdateListRequest request, Guid userId);
         Task<bool> DeleteListaAsync(Guid id, Guid userId);

    }
}
