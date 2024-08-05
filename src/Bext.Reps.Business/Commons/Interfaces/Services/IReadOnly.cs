namespace Bext.Reps.Business.Commons.Interfaces.Services;
public interface IReadOnlyRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByCodigoAsync(string codigo);
    Task InvalidateCacheAsync(string key);
}
