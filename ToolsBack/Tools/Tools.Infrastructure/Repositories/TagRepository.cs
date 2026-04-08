using Microsoft.EntityFrameworkCore;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;
using Tools.Infrastructure.Context;
using static Azure.Core.HttpHeader;

namespace Tools.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }

        public async Task<List<Tag>?> GetTagByNameAsync(List<string> name)
        {
            var normalizedNames = name
        .Where(n => !string.IsNullOrWhiteSpace(n))
        .Select(n => n.Trim().ToLower())
        .Distinct()
        .ToList();

            return await _context.Tags
                .Where(t => normalizedNames.Contains(t.Name))
                .ToListAsync();
        }
    }
}
