
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Validators;
public abstract class TerceroPrestadorBaseValidator : AbstractValidator<TerceroPrestadorRequest>
{
    protected TerceroPrestadorBaseValidator()
    {
        RuleFor(x => x.TipoIdentificacion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.NumeroIdentificacion)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.Departamento)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.Municipio)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(5).WithMessage(DefaultMessage.MaxLength + "5");

        RuleFor(x => x.Direccion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(250).WithMessage(DefaultMessage.MaxLength + "250")
            .MinimumLength(5).WithMessage(DefaultMessage.MinLength + "5");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El correo electrónico no es válido.")
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");

        RuleFor(x => x.TelefonoMovil)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .Matches(DefaultMessage.IsValidPhone).WithMessage("El número de teléfono móvil no es válido.")
            .MaximumLength(15).WithMessage(DefaultMessage.MaxLength + "15")
            .MinimumLength(7).WithMessage(DefaultMessage.MinLength + "7");

        RuleFor(x => x.TelefonoFijo)
            .Matches(DefaultMessage.IsValidPhone).WithMessage("El número de teléfono fijo no es válido.")
            .MaximumLength(10).WithMessage(DefaultMessage.MaxLength + "10")
            .MinimumLength(7).WithMessage(DefaultMessage.MinLength + "7");

        RuleFor(x => x.TelefonoFax)
            .Matches(DefaultMessage.IsValidPhone).WithMessage("El número de fax no es válido.")
            .MaximumLength(10).WithMessage(DefaultMessage.MaxLength + "10")
            .MinimumLength(7).WithMessage(DefaultMessage.MinLength + "7");

        RuleFor(x => x.SitioWeb)
            .MaximumLength(150).WithMessage(DefaultMessage.MaxLength + "150");

        RuleFor(x => x.NumeroIdentificacion)
          .MaximumLength(15).WithMessage(DefaultMessage.MaxLength + "15");
    }
}
public class TerceroPrestadorPNValidator : TerceroPrestadorBaseValidator
{
    public TerceroPrestadorPNValidator()
    {
        RuleFor(x => x.TipoPersona)
            .Equal("PN").WithMessage("TipoPersona debe ser 'PN' para Persona Natural.")
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");


        RuleFor(x => x.PrimerNombre)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MinimumLength(2).WithMessage(DefaultMessage.MinLength + "2");

        RuleFor(x => x.SegundoNombre)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MinimumLength(2).WithMessage(DefaultMessage.MinLength + "2");

        RuleFor(x => x.PrimerApellido)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MinimumLength(2).WithMessage(DefaultMessage.MinLength + "2");

        RuleFor(x => x.SegundoApellido)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MinimumLength(2).WithMessage(DefaultMessage.MinLength + "2");
    }
}

public class TerceroPrestadorPJValidator : TerceroPrestadorBaseValidator
{
    public TerceroPrestadorPJValidator()
    {
        RuleFor(x => x.TipoPersona)
            .Equal("PJ").WithMessage("TipoPersona debe ser 'PJ' para Persona Jurídica.")
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.RazonSocial)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(250).WithMessage(DefaultMessage.MaxLength + "250")
            .MinimumLength(2).WithMessage(DefaultMessage.MinLength + "2");
    }
}
