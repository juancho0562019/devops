

using Bext.Reps.Domain.Entities.DirectorioGeneral;

namespace Bext.Reps.Business.Commons.Interfaces.Services;

public interface IDepartamentoClient : IRestClient 
{
    Task<List<ItemTablaReferencia>> GetDepartamentosAsync();
    Task<ItemTablaReferencia> GetDepartamentosByIdAsync(string codigo);
}
public interface IMunicipioClient : IRestClient 
{
    Task<List<ItemTablaReferencia>> GetMunicipiosAsync();
    Task<ItemTablaReferencia> GetMunicipiosByIdAsync(string codigo);
}
public interface IRestDirectorioGeneralClientFactory
{
    IDepartamentoClient CreateDepartamentoClient();
    IMunicipioClient CreateMunicipioClient();
}
