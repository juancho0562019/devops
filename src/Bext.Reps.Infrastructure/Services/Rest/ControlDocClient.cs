using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Domain.Entities.DirectorioGeneral;

namespace Bext.Reps.Infrastructure.Services.Rest;
public class ControlDocClient : RestClient, IControlDocClient
{
    public ControlDocClient(ClientOptions options) : base(options) { }

    public async Task<ControlDocResponse> GetTokenAsync(string usuario, string contrasena)
    {
        var bodyParams = new Dictionary<string, string>
        {
            { "usuario", usuario },
            { "contrasena", contrasena }
        };

        var result = await PostAsync<ControlDocResponse>("ServiciosApi/SuperSaludApi_ObtenerToken", bodyParams);
        return result ?? new ControlDocResponse();
    }

    public async Task<ControlDocResponse> RadicarDocumentoAsync(RadicadoRequest request)
    {
        var result = await PostAsync<ControlDocResponse>("ServiciosApi/ServiciosApi_Radicar", request);
        return result ?? new ControlDocResponse();
    }
}
