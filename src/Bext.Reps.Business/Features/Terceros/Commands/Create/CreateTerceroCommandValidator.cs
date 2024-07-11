using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Terceros.Commands.Create;
public class CreateTerceroCommandValidator : AbstractValidator<CreateTerceroCommand>
{
    public CreateTerceroCommandValidator()
    {
        RuleFor(v => v.TipoIdentificacion)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(v => v.PrimerNombre)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(20).WithMessage(DefaultMessage.MaxLength + "20");

        RuleFor(v => v.PrimerApellido)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(20).WithMessage(DefaultMessage.MaxLength + "20");

        RuleFor(v => v.Pais)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(v => v.Departamento)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(v => v.Municipio)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(3).WithMessage(DefaultMessage.MaxLength + "3");

        RuleFor(v => v.Email)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .EmailAddress().WithMessage(DefaultMessage.IsEmail)
            .MaximumLength(50);
    }
}
