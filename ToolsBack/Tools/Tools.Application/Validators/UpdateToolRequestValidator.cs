using FluentValidation;
using Tools.Application.DTOs.Tools;

namespace Tools.Application.Validators
{
    public class UpdateToolRequestValidator : AbstractValidator<UpdateToolRequest>
    {
        public UpdateToolRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é requerido.")
                .MinimumLength(3).WithMessage("Nome deve conter ao menos 3 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição é requerida.")
                .MinimumLength(5).WithMessage("Descrição deve conter ao menos 5 caracteres.");

             RuleFor(x => x.Tags)
             .Must(tags => tags == null || tags.All(tag => !string.IsNullOrWhiteSpace(tag)))
             .WithMessage("Tags não podem conter valores vazios ou apenas espaços em branco.");
        }
    }
}
