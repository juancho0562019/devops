using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bext.Reps.Business.Commons.Exceptions;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Infrastructure.Services.Rest.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bext.Reps.Infrastructure.Services.Rest;
public abstract class RestClient : IRestClient
{
    private readonly ClientOptions? _options;

    protected readonly JsonSerializerSettings DefaultSerializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
    };

    private HttpClient? _httpClient;

    protected RestClient(ClientOptions options)
    {
        _options = MergeOptions(options);
    }

    public async Task<T?> GetAsync<T>(
        string uri,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(uri, HttpMethod.Get, queryParams, headers, cancellationToken: cancellationToken);
        return await response.ParseStreamAsync<T>(serializerSettings);
    }

    public async Task<T?> PostFormDataAsync<T>(
        string uri,
        IDictionary<string, object> formData,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default)
    {
        void AttachContent(HttpRequestMessage httpRequest)
        {
            var multipartContent = new MultipartFormDataContent();
            foreach (var item in formData)
            {
                var stringValue = item.Value?.ToString() ?? string.Empty;
                var stringContent = new StringContent(stringValue, Encoding.UTF8);
                multipartContent.Add(stringContent, item.Key);
            }
            httpRequest.Content = multipartContent;
        }

        var response = await SendAsync(uri, HttpMethod.Post, queryParams, headers, AttachContent, cancellationToken);
        return await response.ParseStreamAsync<T>(serializerSettings);
    }

    public async Task<T?> PostAsync<T>(
        string uri,
        object body,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default)
    {
        void AttachContent(HttpRequestMessage httpRequest)
        {
            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(body, DefaultSerializerSettings), Encoding.UTF8, "application/json");
        }

        var response = await SendAsync(uri, HttpMethod.Post, queryParams, headers, AttachContent, cancellationToken);
        return await response.ParseStreamAsync<T>(serializerSettings);
    }

    public async Task<T?> PatchAsync<T>(
        string uri,
        object body,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        JsonSerializerSettings? serializerSettings = null,
        CancellationToken cancellationToken = default)
    {
        void AttachContent(HttpRequestMessage httpRequest)
        {
            var serializedBody = JsonConvert.SerializeObject(body, DefaultSerializerSettings);
            httpRequest.Content = new StringContent(serializedBody, Encoding.UTF8, "application/json");
        }

        var response = await SendAsync(uri, new HttpMethod("PATCH"), queryParams, headers, AttachContent, cancellationToken);
        return await response.ParseStreamAsync<T>(serializerSettings);
    }

    public async Task DeleteAsync(
        string uri,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
    {
        await SendAsync(uri, HttpMethod.Delete, queryParams, headers, null, cancellationToken);
    }

    private static ClientOptions MergeOptions(ClientOptions options)
    {
        return new ClientOptions
        {
            BaseUrl = options.BaseUrl,
            ApiVersion = options.ApiVersion
        };
    }

    protected async Task<ApiException> BuildException(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return new ApiException(response.StatusCode, content);
    }

    private async Task<HttpResponseMessage> SendAsync(
        string requestUri,
        HttpMethod httpMethod,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, string>? headers = null,
        Action<HttpRequestMessage>? attachContent = null,
        CancellationToken cancellationToken = default)
    {
        EnsureHttpClient();

        requestUri = AddQueryString(requestUri, queryParams);

        using var httpRequest = new HttpRequestMessage(httpMethod, requestUri);
        httpRequest.Headers.Add("Api-Version", _options?.ApiVersion);

        if (headers != null)
        {
            AddHeaders(httpRequest, headers);
        }

        attachContent?.Invoke(httpRequest);

        if (_httpClient is null)
            throw new ArgumentException("Instancia http no valida");

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw await BuildException(response);
        }

        return response;
    }

    private static void AddHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }
    }

    private void EnsureHttpClient()
    {
        if (_httpClient != null)
        {
            return;
        }

        var pipeline = new LoggingHandler { InnerHandler = new HttpClientHandler() };

        _httpClient = new HttpClient(pipeline);
        _httpClient.BaseAddress = new Uri(_options?.BaseUrl ?? "");
    }

    private static string AddQueryString(string uri, IDictionary<string, string>? queryParams)
    {
        return queryParams == null ? uri : QueryHelpers.AddQueryString(uri, queryParams);
    }
}

