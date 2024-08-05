
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.Extensions;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Infrastructure.Services;
public class DocumentoService : IDocumentoService
{
    private readonly IControlDocClient _controlDocClient;

    public DocumentoService(IControlDocClient controlDocClient)
    {
        _controlDocClient = controlDocClient;
    }

    public async Task<Result<string?>> AgregarDocumentoAsync(string nombreDocumento, string extension, string archivoBase64, TerceroControlDoc tercero, TipoDocumento? tipoDocumento)
    {

        tipoDocumento = tipoDocumento.ValidateNull(nameof(TipoDocumento));

        var responseToken = await _controlDocClient.GetTokenAsync("usuario", "contrasena");
        if (string.IsNullOrEmpty(responseToken.ValoresRespuesta))
        {
            
            return Result<string?>.Failure("No se pudo cargar el documento");
        }

        var requestControlDoc = new RadicadoRequest
            .Builder()
            .ConAsunto("Documento Prestador " + tipoDocumento.Nombre)
            .ConPrioridad("1")
            .ConFolios("1")
            .ConIdTRDC(0)
            .ConTercero(tercero)
            .Build();

        var anexo = new Anexo()
        {
            NombreArchivo = nombreDocumento,
            NombreAnexo = tipoDocumento.Nombre,
            ExtensionArchivo = extension,
            ArchivoBase64 = archivoBase64
        };

        requestControlDoc.AddAnexo(anexo);
        var radicarResponse = await _controlDocClient.RadicarDocumentoAsync(requestControlDoc);
        if (radicarResponse is null || !radicarResponse.Respuesta)
        {
          
            return Result<string?>.Failure("No se pudo cargar el documento");
        }

        return Result<string?>.Success(radicarResponse.Mensaje);
    }
}
