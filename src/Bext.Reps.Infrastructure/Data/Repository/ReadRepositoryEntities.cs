
using System.Linq.Expressions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Commons.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Bext.Reps.Infrastructure.Data.Repository;
public class ReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : notnull
{
    private readonly IRepsDbContext _context;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30); // TODO: Leer valor de archivo configuración

    public ReadOnlyRepository(IRepsDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }
    public async Task<bool> ExistByIdAsync(TKey id)
    {
        return await GetByIdAsync(id) != null;
    }

    public async Task<bool> ExistByIdAsync(Func<TEntity, bool> filter, params object[] args)
    {
        var vm = await GetAllAsync(filter, args);

        return vm != null && vm.Any();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
    {
        string cacheKey = GenerateCacheKey(typeof(TEntity).Name, id, includes);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            IQueryable<TEntity> query = _context.Set<TEntity, TKey>();

           
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(entity => EF.Property<TKey>(entity, "Id")!.Equals(id));
        });
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync(Func<TEntity, bool> filter, params object[] args)
    {
        string cacheKey = GenerateCacheKey(typeof(TEntity).Name, args);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            var entities = await _context.Set<TEntity, TKey>().ToListAsync();
            return entities.Where(filter).ToList();
        });
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync(Func<TEntity, bool> filter, object[] args, params Expression<Func<TEntity, object>>[] includes)
    {
        string cacheKey = GenerateCacheKey(typeof(TEntity).Name, args, includes);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            IQueryable<TEntity> query = _context.Set<TEntity, TKey>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entities = await query.ToListAsync();
            return entities.Where(filter).ToList();
        });
    }
    private string GenerateCacheKey(string entityName, object[] args, params Expression<Func<TEntity, object>>[] includes)
    {
        var includeNames = includes.Select(include => GetPropertyName(include)).ToArray();
        string includesPart = string.Join("-", includeNames);

        string argsPart = string.Join("-", args.Select(arg => arg?.ToString() ?? "null"));

        return $"{entityName}-{includesPart}-{argsPart}";
    }

    private string GetPropertyName(Expression<Func<TEntity, object>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
        {
            return operand.Member.Name;
        }
        throw new InvalidOperationException("Invalid expression");
    }
    private string GenerateCacheKey(string baseKey, params object[] args)
    {
        return $"{baseKey}-{string.Join("-", args.Where(a => a != null))}";
    }
}
