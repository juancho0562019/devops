using Bext.Reps.Domain.Entities.DirectorioGeneral;

namespace Bext.Reps.Business.Commons.Interfaces.Services;
public interface IDirectorioGeneralRepository
{
    Task<IEnumerable<ItemTablaReferencia>?> GetDepartamentosAsync(Func<ItemTablaReferencia, bool> filter, params object[] args);
    Task<IEnumerable<ItemTablaReferencia>?> GetMunicipiosAsync(Func<ItemTablaReferencia, bool> filter, params object[] args);

    Task<ItemTablaReferencia?> GetDepartamentosByIdAsync(string codigo);

    Task<ItemTablaReferencia?> GetMunicipiosByIdAsync(string codigo);
}
