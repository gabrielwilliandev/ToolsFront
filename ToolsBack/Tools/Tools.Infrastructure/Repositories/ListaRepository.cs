using Microsoft.EntityFrameworkCore;
using Tools.Application.Interfaces;
using Tools.Domain.Entities;
using Tools.Infrastructure.Context;

namespace Tools.Infrastructure.Repositories
{
    public class ListaRepository : IListaRepository
    {

        private readonly AppDbContext _context;

        public ListaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Lista lista)
        {
            _context.Listas.Add(lista);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Lista lista)
        {
            _context.Listas.Remove(lista);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Lista>> GetAllByUserAsync(Guid userId)
        {
            return await _context.Listas.Where(l => l.UserId == userId).ToListAsync();
        }

        public async Task<Lista?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Listas.Include(l => l.Tools)
                .ThenInclude(t => t.Tags)
                .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        }

        public async Task UpdateAsync(Lista lista)
        {
            await _context.SaveChangesAsync();
        }
    }
}
