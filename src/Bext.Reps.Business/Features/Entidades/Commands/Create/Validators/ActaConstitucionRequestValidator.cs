using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Validators;
public class ActaConstitucionRequestConAnimoDeLucroValidator : AbstractValidator<ActaConstitucionRequest>
{
    public ActaConstitucionRequestConAnimoDeLucroValidator(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository)
    {
        RuleFor(x => x)
            .NotNull().WithMessage("El objeto ActaConstitucionRequest no puede ser nulo");

        When(x => x != null, () =>
        {
            RuleFor(x => x.ActoConstitucion)
                .Equal("01").WithMessage("Acto de constitucion debe ser Matricula Mercantil");

            RuleFor(x => x.NumeroActo)
                .NotNull().WithMessage(DefaultMessage.IsRequired)
                .NotEmpty().WithMessage(DefaultMessage.IsRequired)
                .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50");

            RuleFor(x => x.ActoConstitucion)
                .NotEmpty().WithMessage(DefaultMessage.IsRequired)
                .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
                .MustAsync(async (actoConstitucion, cancellation) =>
                    await documentoConstitucionRepository.ExistByIdAsync(actoConstitucion))
                .WithMessage(DefaultMessage.NotFoundMessage("Not found"));

            RuleFor(x => x.EntidadExpide)
                .NotEmpty().WithMessage(DefaultMessage.IsRequired)
                .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");

            RuleFor(x => x.CiudadExpedicion)
                .NotEmpty().WithMessage(DefaultMessage.IsRequired)
                .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");
        });
    }
}
public class ActaConstitucionRequestSinAnimoDeLucroValidator : AbstractValidator<ActaConstitucionRequest>
{
    public ActaConstitucionRequestSinAnimoDeLucroValidator(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository)
    {
        RuleFor(x => x.ActoConstitucion)
            .Must(acto => acto == "02" || acto == "03")
            .WithMessage("Acto de constitucion debe ser Resolucion o Acto Administrativo");

        RuleFor(x => x.NumeroActo)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50");

        RuleFor(x => x.ActoConstitucion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MustAsync(async (actoConstitucion, cancellation) =>
                await documentoConstitucionRepository.ExistByIdAsync(actoConstitucion))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(DocumentoConstitucion)));

        RuleFor(x => x.FechaActo)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.EntidadExpide)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");

        RuleFor(x => x.CiudadExpedicion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");
    }
}

public class ActaConstitucionRequestEntidadesDeDerechoPublicoValidator : AbstractValidator<ActaConstitucionRequest>
{
    public ActaConstitucionRequestEntidadesDeDerechoPublicoValidator(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository)
    {
        RuleFor(x => x.ActoConstitucion)
            .Equal("03").WithMessage("Acto de constitucion debe ser Acto Administrativo");

        RuleFor(x => x.NumeroActo)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50");

        RuleFor(x => x.ActoConstitucion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MustAsync(async (actoConstitucion, cancellation) =>
             await documentoConstitucionRepository.ExistByIdAsync(actoConstitucion))
             .WithMessage(DefaultMessage.NotFoundMessage(nameof(DocumentoConstitucion)));

        RuleFor(x => x.FechaActo)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.EntidadExpide)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");

        RuleFor(x => x.CiudadExpedicion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");
    }
}

public class ActaConstitucionRequestMixtaValidator : AbstractValidator<ActaConstitucionRequest>
{
    public ActaConstitucionRequestMixtaValidator(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository)
    {

        RuleFor(x => x.NumeroActo)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50");

        RuleFor(x => x.ActoConstitucion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MustAsync(async (actoConstitucion, cancellation) =>
            await documentoConstitucionRepository.ExistByIdAsync(actoConstitucion))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(DocumentoConstitucion)));

        RuleFor(x => x.FechaActo)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.EntidadExpide)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");

        RuleFor(x => x.CiudadExpedicion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");
    }
}


public class ActaConstitucionRequestPublicaValidator : AbstractValidator<ActaConstitucionRequest>
{
    public ActaConstitucionRequestPublicaValidator(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository,
                                                   IReadOnlyRepository<CaracterTerritorial, string> caracterTerritorialRespository,
                                                   IReadOnlyRepository<NivelAtencion, int> nivelAtencionRepository)
    {
        RuleFor(x => x.NumeroActo)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50");

        RuleFor(x => x.CaracterTerritorial)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(250).WithMessage(DefaultMessage.MaxLength + "250")
            .MustAsync(async (caracterTerritorial, cancellation) =>
             await caracterTerritorialRespository.ExistByIdAsync(caracterTerritorial))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(CaracterTerritorial)));

        RuleFor(x => x.NivelAtencion)
         .NotNull().WithMessage(DefaultMessage.IsRequired)
         .MustAsync(async (nivelAtencion, cancellation) =>
             nivelAtencion.HasValue && await nivelAtencionRepository.ExistByIdAsync(nivelAtencion.Value))
         .WithMessage(DefaultMessage.NotFoundMessage(nameof(NivelAtencion)));

        RuleFor(x => x.EmpresaSocialEstado)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(250).WithMessage(DefaultMessage.MaxLength + "250");

        RuleFor(x => x.ActoConstitucion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50")
            .MustAsync(async (actoConstitucion, cancellation) =>
            await documentoConstitucionRepository.ExistByIdAsync(actoConstitucion))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(DocumentoConstitucion)));

        RuleFor(x => x.FechaActo)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.EntidadExpide)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");

        RuleFor(x => x.CiudadExpedicion)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(100).WithMessage(DefaultMessage.MaxLength + "100");
    }
}

