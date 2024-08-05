

using Bext.Reps.Business.Commons.Models;

namespace Bext.Reps.Business.Commons.Interfaces.Services;

public interface IControlDocClient : IRestClient 
{
    Task<ControlDocResponse> GetTokenAsync(string usuario, string contrasena);
    Task<ControlDocResponse> RadicarDocumentoAsync(RadicadoRequest request);
}
