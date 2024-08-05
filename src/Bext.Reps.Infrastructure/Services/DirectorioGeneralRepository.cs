using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Domain.Entities.DirectorioGeneral;
using Microsoft.Extensions.Caching.Memory;

namespace Bext.Reps.Infrastructure.Services;
public class DirectorioGeneralRepository : IDirectorioGeneralRepository
{
    private readonly IDepartamentoClient _departamentoClient;
    private readonly IMunicipioClient _municipioClient;
    private readonly IMemoryCache _cache;
    private static readonly string DepartamentosCacheKey = "DepartamentosCacheKey";
    private static readonly string MunicipiosCacheKey = "MunicipiosCacheKey";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30); // TODO: Leer valor de archivo configuración

    public DirectorioGeneralRepository(IDepartamentoClient departamentoClient, IMunicipioClient municipioClient, IMemoryCache cache)
    {
        _departamentoClient = departamentoClient;
        _municipioClient = municipioClient;
        _cache = cache;
    }
    public async Task<ItemTablaReferencia?> GetDepartamentosByIdAsync(string codigo)
    {
        string cacheKey = GenerateCacheKey(DepartamentosCacheKey, codigo);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            var departamento = await _departamentoClient.GetDepartamentosByIdAsync(codigo);
            return departamento;
        });
    }

    public async Task<IEnumerable<ItemTablaReferencia>?> GetDepartamentosAsync(Func<ItemTablaReferencia, bool> filter, params object[] args)
    {
        string cacheKey = GenerateCacheKey(DepartamentosCacheKey, args);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            var departamentos = await _departamentoClient.GetDepartamentosAsync();
            return departamentos?.Where(filter) ?? Enumerable.Empty<ItemTablaReferencia>();
        });
    }

    public async Task<IEnumerable<ItemTablaReferencia>?> GetMunicipiosAsync(Func<ItemTablaReferencia, bool> filter, params object[] args)
    {
        string cacheKey = GenerateCacheKey(MunicipiosCacheKey, args);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            var municipios = await _municipioClient.GetMunicipiosAsync();
            return municipios?.Where(filter) ?? Enumerable.Empty<ItemTablaReferencia>();
        });
    }

    public async Task<ItemTablaReferencia?> GetMunicipiosByIdAsync(string codigo)
    {
        string cacheKey = GenerateCacheKey(MunicipiosCacheKey, codigo);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            var municipio = await _municipioClient.GetMunicipiosByIdAsync(codigo);
            return municipio;
        });
    }
    private string GenerateCacheKey(string baseKey, params object[] args)
    {
        return $"{baseKey}-{string.Join("-", args.Where(a => a != null))}";
    }
}
