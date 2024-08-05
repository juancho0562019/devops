using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Validators;
public class RepresentanteValidator : AbstractValidator<RepresentanteRequest>
{
    public RepresentanteValidator(IReadOnlyRepository<TipoVinculacion, string> tipoVinculacionRepository)
    {
       RuleFor(x => x.TipoIdentificacion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.NumeroIdentificacion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(30).WithMessage(DefaultMessage.MaxLength + "30");

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


public class RepresentantePJPublicaValidator : AbstractValidator<RepresentanteRequest>
{
    public RepresentantePJPublicaValidator(IReadOnlyRepository<TipoVinculacion, string> tipoVinculacionRepository)
    {
        RuleFor(x => x.TipoIdentificacion)
             .NotEmpty().WithMessage(DefaultMessage.IsRequired)
             .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.NumeroIdentificacion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(30).WithMessage(DefaultMessage.MaxLength + "30");

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

        When(v => v.TipoRepresentacion != TipoRepresentacion.Otro, () => 
        {
            RuleFor(x => x.TipoVinculacion)
                .NotNull().WithMessage(DefaultMessage.IsRequired);

            RuleFor(x => x.FechaVinculacion)
                .NotNull().WithMessage(DefaultMessage.IsRequired);

            RuleFor(x => x.TipoVinculacion)
             .MustAsync(async (tipoVinculacion, cancellation) =>
                 tipoVinculacion != null && await tipoVinculacionRepository.ExistByIdAsync(tipoVinculacion))
             .WithMessage("El Tipo de Vinculación no existe.");
        });


    }
}

