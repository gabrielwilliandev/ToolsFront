using FluentValidation;
using Tools.Application.DTOs.Auth;

namespace Tools.Application.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres.")
                .MaximumLength(100).WithMessage("A senha deve conter no máximo 100 caracteres.");
            RuleFor(x => x.ConfirmPass)
                .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
                .Equal(x => x.Password).WithMessage("As senhas devem ser iguais.");
        }
    }
}
