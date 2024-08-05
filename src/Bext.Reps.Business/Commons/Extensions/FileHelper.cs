using Microsoft.AspNetCore.Http;

namespace Bext.Reps.Business.Commons.Extensions;
public static class FileHelper
{
    public static async Task<string> ConvertToBase64(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("El archivo no es válido");
        }

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }
}
