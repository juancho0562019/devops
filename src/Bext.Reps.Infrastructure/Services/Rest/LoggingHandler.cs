using Serilog;

namespace Bext.Reps.Infrastructure.Services.Rest;
public class LoggingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Log.Information("Request: {Request}", request);

        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            Log.Information("Response: {Response}", response);

            return response;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to get response: {Exception}", ex);

            throw;
        }
    }
}
