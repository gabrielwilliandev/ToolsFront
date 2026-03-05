using Microsoft.EntityFrameworkCore;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;
using Tools.Infrastructure.Context;

namespace Tools.Infrastructure.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly ToolsDbContext _context;

        public ToolRepository(ToolsDbContext context)
        {
            _context = context;
        }

        public async Task AddToolAsync(Tool tool)
        {
            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();
        }

        public async Task<Tool?> GetToolByIdAsync(Guid id)
        {
            return await _context.Tools
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void RemoveTool(Tool tool)
        {
            _context.Tools.Remove(tool);
        }

        public async Task<List<Tool>> GetAllAsync()
        {
            return await _context.Tools
                .Include(t => t.Tags)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tool>> SearchAsync(string query)
        {
            query = query.Trim();

            return await _context.Tools
                .Include(t => t.Tags)
                .Where(t =>
                    EF.Functions.Like(t.Name, $"%{query}%") ||
                    EF.Functions.Like(t.Description, $"%{query}%") ||
                    t.Tags.Any(tag => EF.Functions.Like(tag.Name, $"%{query}%"))
                )
                .ToListAsync();
        }
    }
}