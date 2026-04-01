using Microsoft.EntityFrameworkCore;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;
using Tools.Infrastructure.Context;

namespace Tools.Infrastructure.Repositories
{
    public class ToolRepository(AppDbContext dbContext) : IToolRepository
    {
        private readonly AppDbContext _context = dbContext;

        public async Task AddToolAsync(Tool tool)
        {
            _context.Tools.Add(tool);
            await Task.CompletedTask;
        }

        public async Task<Tool?> GetToolByIdAsync(Guid id, Guid userId)
        {
            return await _context.Tools
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void RemoveTool(Tool tool)
        {
            _context.Tools.Remove(tool);
        }

        public async Task<List<Tool>> GetAllAsync(Guid userId)
        {
            return await _context.Tools
                .Include(t => t.Tags)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tool>> SearchAsync(string query, Guid userId)
        {
            query = query.Trim();

            return await _context.Tools
                .Include(t => t.Tags)
                .Where(t =>
                t.UserId == userId &&(
                    EF.Functions.Like(t.Name, $"%{query}%") ||
                    EF.Functions.Like(t.Description, $"%{query}%") ||
                    t.Tags.Any(tag => EF.Functions.Like(tag.Name, $"%{query}%"))
                )
                )
                .ToListAsync();
        }
        public async Task<Tool?> GetToolByIdWithTagsAsync(Guid id)
        {
            return await _context.Tools
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}