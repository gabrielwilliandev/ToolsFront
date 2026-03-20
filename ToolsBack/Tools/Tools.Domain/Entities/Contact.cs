using Tools.Domain.Enums;

namespace Tools.Domain.Entities
{
    public class Contact
    {
        public Guid Id { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string UserEmail { get; private set; }
        public ContactCategory Category { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ContactStatus Status { get; private set; }
        protected Contact() { }

        public Contact(string subject, string body, string userEmail, ContactCategory category)
        {
            Id = Guid.NewGuid();
            Subject = subject;
            Body = body;
            UserEmail = userEmail;
            Category = category;
            CreatedAt = DateTime.UtcNow;
            Status = ContactStatus.Pending;
        }

        public void MarkAsSent()
        {
            Status = ContactStatus.EmailSent;
        }

        public void MarkAsFailed()
        {
            Status = ContactStatus.EmailFailed;
        }
    }
}