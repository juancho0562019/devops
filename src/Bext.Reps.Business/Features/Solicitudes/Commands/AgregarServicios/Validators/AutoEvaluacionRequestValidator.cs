using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios.Validators;
public class AutoEvaluacionRequestValidator : AbstractValidator<AutoEvaluacionRequest>
{
    public AutoEvaluacionRequestValidator(IReadOnlyRepository<Estandar, int> estandarRepository,
                                          IReadOnlyRepository<Criterio, int> criterioRepository) 
    {
        RuleFor(v => v.CriterioId)
            .MustAsync(async (criterio, CancellationToken) => await criterioRepository.ExistByIdAsync(criterio)).WithMessage(DefaultMessage.NotFoundMessage(nameof(Criterio)));
    }
}
