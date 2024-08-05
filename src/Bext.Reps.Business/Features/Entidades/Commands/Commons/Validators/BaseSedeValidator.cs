using Bext.Reps.Business.Features.Entidades.Commands.Commons.Request;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.Commons.Validators;
public abstract class BaseSedeValidator<TSede> : AbstractValidator<TSede> where TSede : BaseSedeRequest
{
    protected BaseSedeValidator()
    {

        RuleFor(x => x.NombreResponsable).Cascade(CascadeMode.Stop)
                    .MaximumLength(80).When(x => !string.IsNullOrEmpty(x.NombreResponsable))
                    .WithMessage(DefaultMessage.MaxLength + "80")
                    .MinimumLength(2).When(x => !string.IsNullOrEmpty(x.NombreResponsable))
                    .WithMessage(DefaultMessage.MinLength + "2");

        RuleFor(x => x.NombreSede).Cascade(CascadeMode.Stop)
                    .MaximumLength(80).When(x => !string.IsNullOrEmpty(x.NombreSede))
                    .WithMessage(DefaultMessage.MaxLength + "80")
                    .MinimumLength(2).When(x => !string.IsNullOrEmpty(x.NombreSede))
                    .WithMessage(DefaultMessage.MinLength + "2");


        RuleFor(x => x.Departamento).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.Municipio).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(5).WithMessage(DefaultMessage.MaxLength + "5");

        RuleFor(x => x.CentroPoblado).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(5).WithMessage(DefaultMessage.MaxLength + "5");

        RuleFor(x => x.Direccion).Cascade(CascadeMode.Stop)
         .NotEmpty().WithMessage(DefaultMessage.IsRequired)
         .MinimumLength(5).WithMessage(DefaultMessage.MinLength + "5")
         .MaximumLength(250).WithMessage(DefaultMessage.MaxLength + "250");


    }

}
