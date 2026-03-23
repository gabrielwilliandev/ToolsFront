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
                      <div style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                      <div style='max-width: 600px; margin: auto; background: #ffffff; border-radius: 10px; padding: 20px; box-shadow: 0 2px 8px rgba(0,0,0,0.1);'>
    
                        <h2 style='color: #4CAF50; text-align: center;'>📩 Novo Contato</h2>

                        <hr style='margin: 20px 0;' />

                        <p><strong>📂 Categoria:</strong> {request.Category}</p>
                        <p><strong>📝 Assunto:</strong> {request.Subject}</p>
                        <p><strong>📧 Email:</strong> {request.UserEmail}</p>

                        <div style='margin-top: 20px;'>
                          <p><strong>💬 Mensagem:</strong></p>
                          <div style='background: #f9f9f9; padding: 15px; border-radius: 5px; border-left: 4px solid #4CAF50;'>
                            {request.Body}
                          </div>
                        </div>

                        <hr style='margin: 20px 0;' />

                        <p style='font-size: 12px; color: #888; text-align: center;'>
                          Este email foi enviado pelo formulário de contato do sistema.
                        </p>

                      </div>
                    </div>";

                await _emailService.SendAsync(
                "killuazoldick143@gmail.com",
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
