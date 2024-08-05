using Bext.Reps.Business.Features.Entidades.Commands.Commons.Validators;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.AgregarSede.Validators;
public class AgregarSedeValidator : BaseSedeValidator<AgregarSedeRequest>
{
    public AgregarSedeValidator():base()
    {
        RuleFor(x => x.NombreSede)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

        RuleFor(x => x.Departamento)
            .NotEmpty().WithMessage("El departamento es obligatorio.")
            .MaximumLength(50).WithMessage("El departamento no puede exceder los 50 caracteres.");

        RuleFor(x => x.Municipio)
            .NotEmpty().WithMessage("El municipio es obligatorio.")
            .MaximumLength(50).WithMessage("El municipio no puede exceder los 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no es válido.")
            .MaximumLength(250).WithMessage("El correo electrónico no puede exceder los 250 caracteres.");

        RuleFor(x => x.TelefonoFijo)
            .MaximumLength(15).WithMessage("El teléfono fijo no puede exceder los 15 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.TelefonoFijo));

        RuleFor(x => x.TelefonoMovil)
            .NotEmpty().WithMessage("El teléfono móvil es obligatorio.")
            .MaximumLength(15).WithMessage("El teléfono móvil no puede exceder los 15 caracteres.");

        RuleFor(x => x.TelefonoFax)
            .MaximumLength(15).WithMessage("El fax no puede exceder los 15 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.TelefonoFax));

    
        RuleFor(x => x.EntidadId)
            .NotEmpty().WithMessage("El ID de la entidad es obligatorio.");
    }
}
