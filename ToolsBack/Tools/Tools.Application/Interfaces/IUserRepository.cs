using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
         Task AddUserAsync(User user);
         Task SaveChangesAsync();
    }
}
