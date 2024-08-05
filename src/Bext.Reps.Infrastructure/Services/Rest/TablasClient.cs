using Bext.Reps.Business.Commons.Interfaces.Services;

using Bext.Reps.Domain.Entities.DirectorioGeneral;

namespace Bext.Reps.Infrastructure.Services.Rest;
public class DepartamentoClient : RestClient, IDepartamentoClient
{
    public DepartamentoClient(ClientOptions options) : base(options) { }

    public async Task<List<ItemTablaReferencia>> GetDepartamentosAsync()
    {
        var result = await GetAsync<TablaReferencia>("api/Departamento");
        return result?.Items ?? new List<ItemTablaReferencia>();
    }

    public async Task<ItemTablaReferencia> GetDepartamentosByIdAsync(string codigo)
    {
        var result = await GetAsync<TablaReferencia>($"api/Departamento/{codigo}");
        return result?.Items.FirstOrDefault() ?? new ItemTablaReferencia();
    }
}

public class MunicipioClient : RestClient, IMunicipioClient
{
    public MunicipioClient(ClientOptions options) : base(options) { }

    public async Task<List<ItemTablaReferencia>> GetMunicipiosAsync()
    {
        var result = await GetAsync<TablaReferencia>("api/Municipio");
        return result?.Items ?? new List<ItemTablaReferencia>();
    }

    public async Task<ItemTablaReferencia> GetMunicipiosByIdAsync(string codigo)
    {
        var result = await GetAsync<TablaReferencia>($"api/Municipio/{codigo}");
        return result?.Items.FirstOrDefault() ?? new ItemTablaReferencia();
    }
}
