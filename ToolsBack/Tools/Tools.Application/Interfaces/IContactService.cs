using Tools.Application.DTOs.Contacts;

namespace Tools.Application.Interfaces
{
    public interface IContactService
    {
        Task SendContactAsync(ContactRequest request);
    }
}
