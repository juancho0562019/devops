using Newtonsoft.Json;

namespace Bext.Reps.Infrastructure.Services.Rest.Extensions;
internal static class HttpResponseMessageExtensions
{
    internal static async Task<T?> ParseStreamAsync<T>(
        this HttpResponseMessage response,
        JsonSerializerSettings? serializerSettings = null)
    {
        using var stream = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        using JsonReader jsonReader = new JsonTextReader(streamReader);

        var sb = await response.Content.ReadAsStringAsync();
        var serializer = serializerSettings == null
            ? JsonSerializer.CreateDefault()
            : JsonSerializer.Create(serializerSettings);

        return serializer.Deserialize<T>(jsonReader);
    }
}
