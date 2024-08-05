using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios.Validators;
public class ServicioRequestValidator : AbstractValidator<ServicioRequest>
{
    public ServicioRequestValidator(IReadOnlyRepository<Servicio, int> servicioRepository) 
    {
        RuleFor(v => v.ServicioId)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage(DefaultMessage.IsRequired)
          .MustAsync(async (servicio, cancellationToken) => await servicioRepository.ExistByIdAsync(servicio))
          .WithMessage(DefaultMessage.NotFoundMessage(nameof(Servicio)));

    }
}
