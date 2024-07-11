using Bext.Reps.Domain.Services;
using Bext.Reps.Infrastructure.Exceptions;
using Bext.Reps.Infrastructure.Extensions;

using Microsoft.AspNetCore.Http;

namespace Bext.Reps.Infrastructure.Services.Users;

public sealed class UserContextReps(IHttpContextAccessor httpContextAccessor) : IUserContextReps
{
    public Guid UserId => httpContextAccessor.HttpContext?.User.GetUserId() ?? throw new UserContextNotAvailableException("Contexto de usuario no encontrado.");
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? throw new UserContextNotAvailableException("Usuario no autenticado.");
    public string UserName => httpContextAccessor.HttpContext?.User.Identity?.Name ?? throw new UserContextNotAvailableException("Nombre de usuario no encontrado.");
    public string Email => httpContextAccessor.HttpContext?.User.GetUserEmail() ?? throw new UserContextNotAvailableException("Email de usuario no encontrado.");
    public bool EsInterno => httpContextAccessor.HttpContext?.User.GetEsInterno() ?? throw new UserContextNotAvailableException("Informacion de usuario no encontrado.");
    public string Rol => httpContextAccessor.HttpContext?.User.GetRol() ?? throw new UserContextNotAvailableException("Informacion de usuario no encontrado.");

}
