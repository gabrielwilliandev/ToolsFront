using WebApplication1.Domain.Entities;

namespace WebApplication1.Infra.Repositories
{
    public interface IToolsRepository
    {
        Task<ToolsEntitie> CreateTool(ToolsEntitie tool, IEnumerable<string> tags);
        public Task<IEnumerable<ToolsEntitie>> GetAll(string? tag = null);
        public Task<ToolsEntitie?> GetById(int id);
        Task<bool> DeleteTool(int id);
    }
}
