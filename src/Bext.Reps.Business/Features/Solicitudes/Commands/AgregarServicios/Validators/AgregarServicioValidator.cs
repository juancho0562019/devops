using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios.Validators;
public class AgregarServicioValidator : AbstractValidator<AgregarServiciosRequest>
{

    public AgregarServicioValidator(IReadOnlyRepository<Entidad, int> entidadRepository,
                                    IReadOnlyRepository<Sede, int> sedeRepository,
                                    IReadOnlyRepository<GrupoServicio, int> grupoServicioRepository,
                                    IReadOnlyRepository<Servicio, int> servicioRepository) : base()
    {
        RuleFor(v => v.EntidadId)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
             .MustAsync(async (entidad, cancellation) =>
                await entidadRepository.ExistByIdAsync(entidad))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(Entidad)));

        RuleFor(v => v.SedeId)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
             .MustAsync(async (sede, cancellation) =>
                await sedeRepository.ExistByIdAsync(sede))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(Sede)));

        RuleFor(v => v.Servicio).SetValidator(new ServicioRequestValidator(servicioRepository));
    }

}
