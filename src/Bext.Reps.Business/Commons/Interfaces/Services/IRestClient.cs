

using Newtonsoft.Json;

namespace Bext.Reps.Business.Commons.Interfaces.Services;
public interface IRestClient
{
    Task<T?> GetAsync<T>(
        string uri,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default);
    Task<T?> PostFormDataAsync<T>(
      string uri,
      IDictionary<string, object> formData,
      IDictionary<string, string>? queryParams = null,
      IDictionary<string, string>? headers = null,
      JsonSerializerSettings? serializerSettings = null,
      CancellationToken cancellationToken = default);
    Task<T?> PostAsync<T>(
        string uri,
        object body,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default);

    Task<T?> PatchAsync<T>(
        string uri,
        object body,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string uri,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default);
}
