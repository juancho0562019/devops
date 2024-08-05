
using System.Net;

namespace Bext.Reps.Business.Commons.Exceptions;
public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Content { get; }

    public ApiException(HttpStatusCode statusCode, string content)
        : base($"Request failed with status code {statusCode}")
    {
        StatusCode = statusCode;
        Content = content;
    }
}
