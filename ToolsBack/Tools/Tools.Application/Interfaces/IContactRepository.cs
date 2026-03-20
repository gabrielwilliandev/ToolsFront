using Tools.Domain.Entities;

namespace Tools.Application.Interfaces
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
    }
}
