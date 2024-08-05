using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Business.Models;

namespace Bext.Reps.Business.Commons.Interfaces.Services;
public interface IDocumentoService
{
    Task<Result<string?>> AgregarDocumentoAsync(string nombreDocumento, string extension, string archivoBase64, TerceroControlDoc tercero, TipoDocumento? tipoDocumento);

}
