using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Validators;
public class CreateEntidadCommandValidator : AbstractValidator<CreateEntidadCommand>
{
    private readonly IReadOnlyRepository<SubTipoNaturaleza, string> _subTipoNaturalezaRepository;
    private readonly ITerceroRepository _terceroRepository;
    private readonly IReadOnlyRepository<TipoPersona, string> _tipoPersonaRepository;
    private readonly IReadOnlyRepository<TipoNaturaleza, string> _tipoNaturalezaRepository;
    private readonly IReadOnlyRepository<ClasePrestador, string> _tipoPrestadorRepository;
    private readonly IReadOnlyRepository<DocumentoConstitucion, string> _documentoConstitucionRepository;
    private readonly IReadOnlyRepository<CaracterTerritorial, string> _caracterTerritorialRespository;
    private readonly IReadOnlyRepository<NivelAtencion, int> _nivelAtencionRepository;
    private readonly IReadOnlyRepository<TipoVinculacion, string> _tipoVinculacionRepository;

    public CreateEntidadCommandValidator(
        ITerceroRepository terceroRepository,
        IReadOnlyRepository<TipoPersona, string> tipoPersonaRepository,
        IReadOnlyRepository<TipoNaturaleza, string> tipoNaturalezaRepository,
        IReadOnlyRepository<ClasePrestador, string> tipoPrestadorRepository,
        IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository,
        IReadOnlyRepository<CaracterTerritorial, string> caracterTerritorialRespository,
        IReadOnlyRepository<NivelAtencion, int> nivelAtencionRepository,
        IReadOnlyRepository<TipoVinculacion, string> tipoVinculacionRepository,
        IReadOnlyRepository<SubTipoNaturaleza, string> subTipoNaturalezaRepository)
    {
        _terceroRepository = terceroRepository;
        _tipoPersonaRepository = tipoPersonaRepository;
        _tipoNaturalezaRepository = tipoNaturalezaRepository;
        _tipoPrestadorRepository = tipoPrestadorRepository;
        _documentoConstitucionRepository = documentoConstitucionRepository;
        _caracterTerritorialRespository = caracterTerritorialRespository;
        _nivelAtencionRepository = nivelAtencionRepository;
        _tipoVinculacionRepository = tipoVinculacionRepository;
        _subTipoNaturalezaRepository = subTipoNaturalezaRepository;

        RuleForProperties();
        RuleForPersonaNatural();
        RuleForPersonaJuridica();
        RuleForSubTipoNaturaleza();
        RuleForSedeRequest();
        RuleForRepresentanteRequest();
    }

    private void RuleForProperties()
    {
        RuleFor(x => x.ClasePrestador)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2")
            .MustAsync(async (clasePrestador, cancellation) =>
                await _tipoPrestadorRepository.ExistByIdAsync(clasePrestador))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(ClasePrestador)));

        RuleFor(x => x.TipoIdentificacion)
           .NotNull().WithMessage(DefaultMessage.IsRequired)
           .NotEmpty().WithMessage(DefaultMessage.IsRequired)
           .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2");

        RuleFor(x => x.NumeroIdentificacion).Cascade(CascadeMode.Stop)
           .NotNull().WithMessage(DefaultMessage.IsRequired)
           .NotEmpty().WithMessage(DefaultMessage.IsRequired)
           .MaximumLength(15).WithMessage(DefaultMessage.MaxLength + "15");

        RuleFor(x => x.RazonSocial)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MinimumLength(2).WithMessage(DefaultMessage.MinLength + "2")
            .MaximumLength(250).WithMessage(DefaultMessage.MaxLength + "250");

        RuleFor(x => x.TipoPersona)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2")
            .MustAsync(async (tipoPersona, cancellation) =>
                await _tipoPersonaRepository.ExistByIdAsync(tipoPersona))
            .WithMessage(DefaultMessage.NotFoundMessage(nameof(TipoPersona)));

        RuleFor(x => x.Correo)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .EmailAddress().WithMessage("Direccion invalida de email");

        RuleFor(x => x.TelefonoFijo)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.Direccion)
             .NotNull().WithMessage(DefaultMessage.IsRequired)
             .NotEmpty().WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.TerceroPrestadorRequest)
            .NotNull().WithMessage(DefaultMessage.IsRequired);
    }

    private void RuleForPersonaNatural()
    {
        When(v => v.TipoPersona == "PN", () =>
        {
            RuleFor(x => x.ClasePrestador)
                .Equal("02").WithMessage("Cuando seleccione Persona Natural debe enviar clase prestador como Profesional Independiente");
        });
    }

    private void RuleForPersonaJuridica()
    {
        When(x => x.TipoPersona == "PJ", () =>
        {
            RuleFor(x => x.TipoNaturaleza)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(DefaultMessage.IsRequired)
                .NotNull().WithMessage(DefaultMessage.IsRequired)
                .MaximumLength(2).WithMessage(DefaultMessage.MaxLength + "2")
                .MustAsync(async (tipoNaturaleza, cancellation) =>
                    await _tipoNaturalezaRepository.ExistByIdAsync(tipoNaturaleza))
                .WithMessage(DefaultMessage.NotFoundMessage(nameof(TipoNaturaleza)));

            When(x => x.TipoNaturaleza == "01", () => RuleForPublica());
            When(x => x.TipoNaturaleza == "02", () => RuleForPrivada());
            When(x => x.TipoNaturaleza == "03", () => RuleForMixta());
        });
    }

    private void RuleForPublica()
    {
        RuleFor(x => x.SubTipoNaturaleza)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MustAsync(ValidarSubTipoNaturaleza)
            .WithMessage("El SubTipoNaturaleza no corresponde a un codigo valido");

        RuleFor(x => x.ActaConstitucion)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .SetValidator(new ActaConstitucionRequestPublicaValidator(_documentoConstitucionRepository, _caracterTerritorialRespository, _nivelAtencionRepository))
            .When(v => v.ActaConstitucion != null);
    }

    private void RuleForPrivada()
    {
        RuleFor(x => x.SubTipoNaturaleza)
            .NotEmpty().WithMessage(DefaultMessage.IsRequired)
            .MustAsync(ValidarSubTipoNaturaleza)
            .WithMessage("El SubTipoNaturaleza no corresponde a un codigo valido");

        When(x => x.SubTipoNaturaleza == "21", () => RuleForPrivadaConAnimoDeLucro());
        When(x => x.SubTipoNaturaleza == "22", () => RuleForPrivadaSinAnimoDeLucro());
        When(x => x.SubTipoNaturaleza == "23", () => RuleForPrivadaEntidadesDeDerechoPublico());
    }

    private void RuleForPrivadaConAnimoDeLucro()
    {
        RuleFor(x => x.ActaConstitucion)
           .NotNull().WithMessage(DefaultMessage.IsRequired)
           .When(x => x.ActaConstitucion != null)
           .WithMessage(DefaultMessage.IsRequired);

        RuleFor(x => x.ActaConstitucion)
            .SetValidator(new ActaConstitucionRequestConAnimoDeLucroValidator(_documentoConstitucionRepository))
            .When(x => x.ActaConstitucion != null);
    }

    private void RuleForPrivadaSinAnimoDeLucro()
    {
        RuleFor(x => x.ActaConstitucion)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .SetValidator(new ActaConstitucionRequestSinAnimoDeLucroValidator(_documentoConstitucionRepository))
            .When(v => v.ActaConstitucion != null);
    }

    private void RuleForPrivadaEntidadesDeDerechoPublico()
    {
        RuleFor(x => x.ActaConstitucion)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .SetValidator(new ActaConstitucionRequestEntidadesDeDerechoPublicoValidator(_documentoConstitucionRepository))
            .When(v => v.ActaConstitucion != null);
    }

    private void RuleForMixta()
    {
        RuleFor(x => x.ActaConstitucion)
            .NotNull().WithMessage(DefaultMessage.IsRequired)
            .SetValidator(new ActaConstitucionRequestMixtaValidator(_documentoConstitucionRepository))
            .When(v => v.ActaConstitucion != null);
    }

    private void RuleForSubTipoNaturaleza()
    {
        RuleFor(x => x.SubTipoNaturaleza)
            .MaximumLength(50).WithMessage(DefaultMessage.MaxLength + "50");
    }

    private void RuleForSedeRequest()
    {
        RuleFor(x => x.SedeRequest)
            .Must(NoMoreThanOnePrincipal)
            .When(x => x.SedeRequest != null)
            .WithMessage("No puede haber más de una sede principal.");

        RuleForEach(x => x.SedeRequest)
            .SetValidator(new SedeRequestValidator())
            .When(x => x.SedeRequest != null);
    }

    private void RuleForRepresentanteRequest()
    {
        When(x => x.TipoPersona == "PJ", () =>
        {
            RuleFor(v => v.RepresentanteRequest)
                .NotNull().WithMessage(DefaultMessage.IsRequired);

            When(v => v.TipoNaturaleza == "01", () =>
            {
                RuleForEach(x => x.RepresentanteRequest)
                    .SetValidator(new RepresentantePJPublicaValidator(_tipoVinculacionRepository))
                    .When(x => x.RepresentanteRequest != null);
            });

            When(v => v.TipoNaturaleza != "01", () =>
            {
                RuleForEach(x => x.RepresentanteRequest)
                    .SetValidator(new RepresentanteValidator(_tipoVinculacionRepository))
                    .When(x => x.RepresentanteRequest != null);
            });
        });
    }

    private bool NoMoreThanOnePrincipal(List<SedeRequest>? sedeRequests)
    {
        if (sedeRequests is null)
            return true;

        return sedeRequests.Count(s => s.EsPrincipal) <= 1;
    }

    private async Task<bool> ValidarSubTipoNaturaleza(CreateEntidadCommand command, string? subTipoNaturaleza, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(subTipoNaturaleza))
            return false;

        if (string.IsNullOrEmpty(command.TipoNaturaleza))
            return false;

        var filter = (SubTipoNaturaleza x) => x.Id == subTipoNaturaleza && x.TipoNaturalezaId == command.TipoNaturaleza;

        return await _subTipoNaturalezaRepository.ExistByIdAsync(filter, subTipoNaturaleza, command.TipoNaturaleza);
    }
}
