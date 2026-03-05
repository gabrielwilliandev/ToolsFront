using Microsoft.EntityFrameworkCore;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;
using Tools.Infrastructure.Context;

namespace Tools.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ToolsDbContext _context;

        public TagRepository(ToolsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }

        public async Task<Tag?> GetTagByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
        }
    }
}
