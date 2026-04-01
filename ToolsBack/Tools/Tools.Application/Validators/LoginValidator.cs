using FluentValidation;
using Tools.Application.DTOs.Auth;

namespace Tools.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email não pode está em branco!")
                .EmailAddress().WithMessage("Formato de email inválido!");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha não pode está em branco!")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres!");
        }
    }
}
