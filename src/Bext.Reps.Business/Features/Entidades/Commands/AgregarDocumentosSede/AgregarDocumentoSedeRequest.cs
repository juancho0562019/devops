using AutoMapper;
using Bext.Reps.Business.Commons.Extensions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using static Bext.Reps.Business.Commons.Models.TerceroControlDoc;

namespace Bext.Reps.Business.Features.Entidades.Commands.AgregarDocumentosSede;
public class AgregarDocumentoSedeRequest : IRequest<Result<string?>>
{
    public int EntidadId { get; set; }
    public int TipoDocumentoId { get; set; }
    public int SedeId { get; set; }
    public IFormFile File { get; set; } = null!;
}
public class AgregarDocumentoSedeHandler : IRequestHandler<AgregarDocumentoSedeRequest, Result<string?>>
{
    private readonly IOptions<GlobalValidFile> _options;
    private readonly IDocumentoService _documentoService;
    private readonly IEntidadRepository _entidadRepository;
    private readonly ISedeRepository _sedeRepository;
    private readonly IReadOnlyRepository<TipoDocumento, int> _tipoDocumentoRepository;
    private readonly IRepsDbContext _context;
    public AgregarDocumentoSedeHandler(
        IOptions<GlobalValidFile> options,
        IDocumentoService documentoService,
        IEntidadRepository entidadRepository,
        ISedeRepository sedeRepository,
        IReadOnlyRepository<TipoDocumento, int> tipoDocumentoRepository,
        IRepsDbContext repsDbContext)
    {
        _options = options;
        _documentoService = documentoService;
        _entidadRepository = entidadRepository;
        _tipoDocumentoRepository = tipoDocumentoRepository;
        _sedeRepository = sedeRepository;
        _context = repsDbContext;
    }

    public async Task<Result<string?>> Handle(AgregarDocumentoSedeRequest request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.File.FileName).ToLower();
        if (!_options.Value.ValidExtension.Contains(extension))
        {
            return Result<string>.Failure(DefaultMessage.BadFormat);
        }
        if (!request.File.IsValidDocument(_options.Value.ValidExtension.ToArray()))
        {
            return Result<string>.Failure(DefaultMessage.BadFormat);
        }
        if (request.File.FileName.Length > 250)
        {
            return Result<string>.Failure("Nombre de archivo supera la cantidad de caracteres permitidos");
        }
        if(await _sedeRepository.EsDocumentoRegistrado(request.SedeId, request.TipoDocumentoId))
            return Result<string>.Failure("Tipo de documento ya registrado para la solicitud");

        var sede = (await _sedeRepository.GetByIdAsync(request.SedeId)).ValidateNull(nameof(request.SedeId));

        sede.ValidateNull(nameof(Sede), $"No se encontro la sede con id {request.SedeId}");

        var entidad = await _entidadRepository.GetByIdWithSedesAsync(request.EntidadId);
        entidad.ValidateNull(nameof(Sede), $"No se encontro la entidad para el EntidadId {request.EntidadId}");

        var tipoDocumento = await _tipoDocumentoRepository.GetByIdAsync(request.TipoDocumentoId);
        tipoDocumento.ValidateNull(nameof(request.TipoDocumentoId), DefaultMessage.NotFoundMessage(nameof(TipoDocumento)));

        if (tipoDocumento != null && tipoDocumento.Tipo != Domain.Commons.Enums.TipoDocumentoPrestador.Sede)
            return Result<string>.Failure("El tipo de documento enviado no corresponde a los requeridos para Registro de Sede");

        var archivoBase64 = await FileHelper.ConvertToBase64(request.File);
        if (string.IsNullOrEmpty(archivoBase64))
        {
            return Result<string>.Failure("No se pudo cargar el documento");
        }

        var tercero = new TerceroBuilder()
            .ConTipoPersona(entidad?.TipoPersona.Nombre)
            .ConTipoIdentificacion(entidad?.Identificacion.TipoIdentificacion)
            .ConNumeroIdentificacion(entidad?.Identificacion.NumeroDocumento)
            .ConNombres(entidad?.Tercero.GetNombre())
            .ConApellidos(entidad?.Tercero.GetNombre())
            .ConPais("Colombia")
            .ConDepartamento(entidad?.Tercero.Ubicacion.Departamento)
            .ConMunicipio(entidad?.Tercero.Ubicacion.Municipio)
            .ConDireccion(entidad?.Direccion)
            .ConCorreo(entidad?.DatosContacto.Email ?? "")
            .ConTelefono(entidad?.DatosContacto.TelefonoFijo ?? "")
            .ConCelular(entidad?.DatosContacto.TelefonoMovil ?? "")
            .ConFax(entidad?.DatosContacto.TelefonoFax ?? "")
            .ConPagina(entidad?.Tercero.DatosContacto.SitioWeb ?? "")
            .ConNaturaleza(entidad?.TipoNaturaleza?.Nombre)
            .Build();

        var result = await _documentoService.AgregarDocumentoAsync(
                                                                request.File.FileName,
                                                                extension,
                                                                archivoBase64,
                                                                tercero,
                                                                tipoDocumento);

        if (result.IsSuccess)
        {
            var documentoEntidad = DocumentoSede.Create(DateTime.UtcNow,  request.File.FileName, tipoDocumento?.Nombre, tipoDocumento.Id);

            if (entidad != null) 
            {
                entidad.AddSedeDocumento(request.SedeId,documentoEntidad);

                _context.Entidades.Update(entidad);
                await _context.SaveChangesAsync(cancellationToken);
                return result;
            }
            
        }
        return result;
    }
}
