using Tools.Domain.Enums;

namespace Tools.Application.DTOs.Contacts
{
    public class ContactRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserEmail { get; set; }
        public ContactCategory Category { get; set; }
    }
}
