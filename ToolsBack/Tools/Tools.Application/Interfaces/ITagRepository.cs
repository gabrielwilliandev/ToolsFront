using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag?> GetTagByNameAsync(string name);
        Task AddAsync(Tag tag);
    }
}
