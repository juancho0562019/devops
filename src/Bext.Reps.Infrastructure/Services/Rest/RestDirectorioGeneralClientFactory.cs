
using Bext.Reps.Business.Commons.Interfaces.Services;

namespace Bext.Reps.Infrastructure.Services.Rest;
public class RestDirectorioGeneralClientFactory : IRestDirectorioGeneralClientFactory
{
    private readonly ClientOptions _options;

    public RestDirectorioGeneralClientFactory(ClientOptions options)
    {
        _options = options;
    }

    public IDepartamentoClient CreateDepartamentoClient()
    {
        return new DepartamentoClient(_options);
    }

    public IMunicipioClient CreateMunicipioClient()
    {
        return new MunicipioClient(_options);
    }
}
