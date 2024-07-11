using Bext.Reps.Infrastructure.Exceptions;

using System.Security.Claims;

namespace Bext.Reps.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? user)
        {
            if (user == null)
            {
                throw new UserInfoNotAvailableException(nameof(user));
            }

            var userId = user.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;

            return Guid.TryParse(userId, out var parsedUserId) ? parsedUserId : throw new UserInfoNotAvailableException("User info is not available.");
        }

        public static string GetUserEmail(this ClaimsPrincipal? user)
        {
            if (user == null)
            {
                throw new UserInfoNotAvailableException(nameof(user));
            }

            var userEmail = user.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;

            return userEmail ?? throw new UserInfoNotAvailableException("User email is not available.");
        }

        public static bool GetEsInterno(this ClaimsPrincipal? user)
        {
            if (user == null)
            {
                throw new UserInfoNotAvailableException(nameof(user));
            }

            var esInterno = user.Claims.FirstOrDefault(c => c.Type.Contains("EsInterno"))?.Value;

            return bool.TryParse(esInterno, out var parsedEsInterno) ? parsedEsInterno : throw new UserInfoNotAvailableException("User information is not available.");
        }

        public static string GetRol(this ClaimsPrincipal? user)
        {
            if (user == null)
            {
                throw new UserInfoNotAvailableException(nameof(user));
            }

            var rol = user.Claims.FirstOrDefault(c => c.Type.Contains("identity/claims/role"))?.Value;

            return rol ?? throw new UserInfoNotAvailableException("User information is not available.");
        }
    }
}
