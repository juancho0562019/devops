using Microsoft.AspNetCore.Http;

namespace Bext.Reps.Business.Commons.Extensions;
public static class HttpPostedFileBaseExtensions
{
    public static bool IsValidDocument(this IFormFile file, string[] validExtensions)
    {
        var validMimeTypes = new[]
        {
            "application/msword",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/pdf"
        };


        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (Array.Exists(validMimeTypes, mimeType => string.Equals(file.ContentType, mimeType, StringComparison.OrdinalIgnoreCase)) &&
            Array.Exists(validExtensions, extension => string.Equals(fileExtension, extension, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }
        

        return false;
    }
}
