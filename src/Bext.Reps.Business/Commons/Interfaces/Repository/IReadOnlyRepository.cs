
using System.Linq.Expressions;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Business.Commons.Interfaces.Repository;
public interface IReadOnlyRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : notnull
{
    Task<bool> ExistByIdAsync(TKey id);
    Task<bool> ExistByIdAsync(Func<TEntity, bool> filter, params object[] args);
    Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>?> GetAllAsync(Func<TEntity, bool> filter, object[] args, params Expression<Func<TEntity, object>>[] includes);
}
