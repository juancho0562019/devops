using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios.Validators;
public class FranjaHorariaRequestValidator : AbstractValidator<FranjaHorariaRequest>
{
    public FranjaHorariaRequestValidator()
    {
        RuleFor(x => x.HoraApertura).NotEmpty().WithMessage(DefaultMessage.IsRequired);
        RuleFor(x => x.HoraCierre).NotEmpty().WithMessage(DefaultMessage.IsRequired);
        RuleFor(x => x.DiasAtencion).NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x).Must(x => x.HoraApertura < x.HoraCierre)
            .WithMessage("La hora de apertura debe ser anterior a la hora de cierre.");
    }
}
