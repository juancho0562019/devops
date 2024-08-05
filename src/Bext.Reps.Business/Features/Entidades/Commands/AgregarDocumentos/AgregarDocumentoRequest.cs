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

namespace Bext.Reps.Business.Features.Entidades.Commands.AgregarDocumentos;
public class AgregarDocumentoRequest : IRequest<Result<string?>>
{
    public int TipoDocumentoId { get; set; }
    public int EntidadId { get; set; }
    public IFormFile File { get; set; } = null!;
}
public class AgregarDocumentoHandler : IRequestHandler<AgregarDocumentoRequest, Result<string?>>
{
    private readonly IOptions<GlobalValidFile> _options;
    private readonly IDocumentoService _documentoService;
    private readonly IEntidadRepository _entidadRepository;
    private readonly IReadOnlyRepository<TipoDocumento, int> _tipoDocumentoRepository;
    private readonly IRepsDbContext _context;
    public AgregarDocumentoHandler(
        IOptions<GlobalValidFile> options,
        IDocumentoService documentoService,
        IEntidadRepository entidadRepository,
        IReadOnlyRepository<TipoDocumento, int> tipoDocumentoRepository,
        IRepsDbContext repsDbContext)
    {
        _options = options;
        _documentoService = documentoService;
        _entidadRepository = entidadRepository;
        _tipoDocumentoRepository = tipoDocumentoRepository;
        _context = repsDbContext;
    }

    public async Task<Result<string?>> Handle(AgregarDocumentoRequest request, CancellationToken cancellationToken)
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
        if (await _entidadRepository.EsDocumentoRegistrado(request.EntidadId, request.TipoDocumentoId))
            return Result<string>.Failure("Tipo de documento ya registrado para la solicitud");

        Entidad entidad = (await _entidadRepository.GetByIdAsync(request.EntidadId)).ValidateNull(nameof(request.EntidadId));
        

        var tipoDocumento = await _tipoDocumentoRepository.GetByIdAsync(request.TipoDocumentoId);
        tipoDocumento.ValidateNull(nameof(request.TipoDocumentoId), DefaultMessage.NotFoundMessage(nameof(TipoDocumento)));

        if (tipoDocumento != null && tipoDocumento.Tipo != Domain.Commons.Enums.TipoDocumentoPrestador.Prestador)
            return Result<string>.Failure("El tipo de documento enviado no corresponde a los requeridos para Registro de Entidad Prestadora de Servicio");

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
            var documentoEntidad = DocumentoEntidad.Create(DateTime.UtcNow, "", request.File.FileName, tipoDocumento?.Nombre, tipoDocumento?.Id);

            if (entidad != null) 
            {
                entidad.AddDocumentoEntidad(documentoEntidad);

                _context.Entidades.Update(entidad);
                await _context.SaveChangesAsync(cancellationToken);
                return result;
            }
            
        }
        return result;
    }
}
