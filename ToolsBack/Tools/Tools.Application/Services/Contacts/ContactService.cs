using FluentValidation;
using Tools.Application.DTOs.Contacts;
using Tools.Application.Interfaces;
using Tools.Application.Notifications;
using Tools.Domain.Entities;

namespace Tools.Application.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IEmailService _emailService;
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<ContactRequest> _validator;
        private readonly NotificationContext _notificationContext;
        public ContactService(IEmailService emailService, IContactRepository contactRepository,
            IValidator<ContactRequest> validator, NotificationContext notificationContext)
        {
            _emailService = emailService;
            _contactRepository = contactRepository;
            _validator = validator;
            _notificationContext = notificationContext;
        }
        public async Task SendContactAsync(ContactRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _notificationContext.AddErrors("validation.error", error.ErrorMessage);
                return;
            }

            if (request == null)
            {
                _notificationContext.AddErrors("request.null", "Requisição inválida");
                return;
            }

            var contact = new Contact(
                request.Subject,
                request.Body,
                request.UserEmail,
                request.Category);

            await _contactRepository.AddContactAsync(contact);
            try
            {


            var message = $@"
                        <h2>Novo Contato</h2>
                        <p><strong>Categoria: </strong>{request.Category}</p>
                        <p><strong>Assunto: </strong>{request.Subject}</p>
                        <p><strong>Descrição: </strong> {request.Body}</p>
                        <p><strong>Email: </strong> {request.UserEmail}</p>";

            await _emailService.SendAsync(
                "seuemail@gmail.com",
                $"[{request.Category}] {request.Subject}",
                message);

                contact.MarkAsSent();
            }
            catch
            {
                contact.MarkAsFailed();
            }
        }
    }
}
