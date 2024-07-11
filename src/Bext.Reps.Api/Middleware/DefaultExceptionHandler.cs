using Bext.Reps.Domain.Commons.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bext.Reps.Api.Middleware;

public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;
    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions =
            {
                ["traceId"] = httpContext?.TraceIdentifier
            }
        };

        switch (exception)
        {
            case ArgumentNullException:
            case NullReferenceException:
            case ArgumentOutOfRangeException:
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = "Por favor revise la solicitud del servicio.";
                
                break;
            case DirectoryNotFoundException:
            case FileNotFoundException:
            case KeyNotFoundException:
            case NotFoundException:
                problemDetails.Status = (int)HttpStatusCode.NotFound;
                problemDetails.Title = "El recurso solicitado no fue encontrado.";
                break;
            case TimeoutException:
                problemDetails.Status = (int)HttpStatusCode.RequestTimeout;
                problemDetails.Title = "Tiempo de espera agotado.";
                break;
            default:
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = "Ha ocurrido un error inesperado.";
                break;
        }

        _logger.LogError(exception, exception.Message);

        ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext)); 
        
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }
}
