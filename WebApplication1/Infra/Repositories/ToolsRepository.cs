using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entities;
using WebApplication1.Infra.Data;

namespace WebApplication1.Infra.Repositories
{
    public class ToolsRepository : IToolsRepository
    {
        private readonly AppDbContext _context;

        public ToolsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ToolsEntitie> CreateTool(ToolsEntitie tool, IEnumerable<string> tags)
        {
            foreach (var tagName in tags)
            {
                var normalized = tagName.Trim().ToLower();

                var tag = await _context.Tags
                    .FirstOrDefaultAsync(t => t.Name == normalized);

                if (tag == null)
                {
                    tag = new TagEntitie { Name = normalized };
                    _context.Tags.Add(tag);
                }

                tool.ToolTags.Add(new ToolTag
                {
                    Tag = tag
                });
            }

            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();

            return tool;
        }

        public Task<bool> DeleteTool(int id)
        {
            _context.Tools.Remove(_context.Tools.Find(id)!);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<ToolsEntitie>> GetAll(string? tag = null)
        {
            var query = _context.Tools
                .Include(t => t.ToolTags)
                .ThenInclude(tt => tt.Tag)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(t =>
                    t.ToolTags.Any(tt => tt.Tag.Name == tag));
            }

            return await query.ToListAsync();

        }

        public async Task<ToolsEntitie?> GetById(int id)
        {
            return await _context.Tools.FirstOrDefaultAsync(t => t.ToolId == id);

        }
    }
}
