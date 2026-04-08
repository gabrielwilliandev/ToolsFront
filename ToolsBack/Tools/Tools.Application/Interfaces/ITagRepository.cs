using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>?> GetTagByNameAsync(List<string> name);
        Task AddAsync(Tag tag);
    }
}
