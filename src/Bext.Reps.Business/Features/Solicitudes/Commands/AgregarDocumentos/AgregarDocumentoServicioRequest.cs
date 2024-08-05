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

namespace Bext.Reps.Business.Features.Solicitudes.Commands.AgregarDocumentos;
public class AgregarDocumentoServicioRequest : IRequest<Result<string?>>
{
    public int TipoDocumentoId { get; set; }
    public int SolicitudId { get; set; }
    public IFormFile File { get; set; } = null!;
}
public class AgregarDocumentoHandler : IRequestHandler<AgregarDocumentoServicioRequest, Result<string?>>
{
    private readonly IOptions<GlobalValidFile> _options;
    private readonly IDocumentoService _documentoService;
    private readonly IReadOnlyRepository<TipoDocumento, int> _tipoDocumentoRepository;
    private readonly IReadOnlyRepository<Solicitud, int> _solicitudRepository;
    private readonly IRepsDbContext _context;
    public AgregarDocumentoHandler(
        IOptions<GlobalValidFile> options,
        IDocumentoService documentoService,
        IEntidadRepository entidadRepository,
        IReadOnlyRepository<TipoDocumento, int> tipoDocumentoRepository,
        IReadOnlyRepository<Solicitud, int> solicitudRepository,
        IRepsDbContext repsDbContext)
    {
        _options = options;
        _documentoService = documentoService;
        _tipoDocumentoRepository = tipoDocumentoRepository;
        _context = repsDbContext;
        _solicitudRepository = solicitudRepository;
    }

    public async Task<Result<string?>> Handle(AgregarDocumentoServicioRequest request, CancellationToken cancellationToken)
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
        var solicitud = await _solicitudRepository.GetByIdAsync(request.SolicitudId, (Solicitud b) => b.Entidad, (Solicitud b) => b.Entidad.TipoPersona, (Solicitud b) => b.Entidad.TipoNaturaleza, (Solicitud b) => b.Entidad.Tercero);
        solicitud.ValidateNull(nameof(request.SolicitudId));

        var tipoDocumento = await _tipoDocumentoRepository.GetByIdAsync(request.TipoDocumentoId);
        tipoDocumento.ValidateNull(nameof(request.TipoDocumentoId), DefaultMessage.NotFoundMessage(nameof(TipoDocumento)));

        if(tipoDocumento != null && tipoDocumento.Tipo != Domain.Commons.Enums.TipoDocumentoPrestador.Servicio)
            return Result<string>.Failure("El tipo de documento enviado no corresponde a los requeridos para Registro de servicio");

        var archivoBase64 = await FileHelper.ConvertToBase64(request.File);
        if (string.IsNullOrEmpty(archivoBase64))
        {
            
            return Result<string>.Failure("No se pudo cargar el documento");
        }

        var tercero = new TerceroBuilder()
            .ConTipoPersona(solicitud?.Entidad.TipoPersona.Nombre)
            .ConTipoIdentificacion(solicitud?.Entidad?.Identificacion.TipoIdentificacion)
            .ConNumeroIdentificacion(solicitud?.Entidad?.Identificacion.NumeroDocumento)
            .ConNombres(solicitud?.Entidad.Tercero.GetNombre())
            .ConApellidos(solicitud?.Entidad.Tercero.GetNombre())
            .ConPais("Colombia")
            .ConDepartamento(solicitud?.Entidad.Tercero.Ubicacion.Departamento)
            .ConMunicipio(solicitud?.Entidad.Tercero.Ubicacion.Municipio)
            .ConDireccion(solicitud?.Entidad.Direccion)
            .ConCorreo(solicitud?.Entidad.DatosContacto.Email ?? "")
            .ConTelefono(solicitud?.Entidad.DatosContacto.TelefonoFijo ?? "")
            .ConCelular(solicitud?.Entidad.DatosContacto.TelefonoMovil ?? "")
            .ConFax(solicitud?.Entidad.DatosContacto.TelefonoFax ?? "")
            .ConPagina(solicitud?.Entidad.Tercero.DatosContacto.SitioWeb ?? "")
            .ConNaturaleza(solicitud?.Entidad?.TipoNaturaleza?.Nombre)
            .Build();

        var result = await _documentoService.AgregarDocumentoAsync(
                                                                request.File.FileName,
                                                                extension,
                                                                archivoBase64,
                                                                tercero,
                                                                tipoDocumento);

        if (result.IsSuccess)
        {
            var documentoSolicitud = DocumentoServicio.Create(DateTime.UtcNow, "", request.File.FileName, tipoDocumento?.Nombre, tipoDocumento?.Id);

            if (solicitud != null) 
            {
                solicitud.AddDocumentoServicio(documentoSolicitud);

                _context.Solicitudes.Update(solicitud);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
        return result;
    }
}
