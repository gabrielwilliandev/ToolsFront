using FluentValidation;
using Tools.Application.DTOs.Contacts;

namespace Tools.Application.Validators
{
    public class ContactValidator : AbstractValidator<ContactRequest>
    {
        public ContactValidator() 
        {
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Assunto é obrigatório!")
                .MaximumLength(100);

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Descrição é obrigatória!");

            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .EmailAddress().WithMessage("Email inválido!");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Caregoria é obrigatória");

            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("Categoria inválida!");
        }
    }
}
