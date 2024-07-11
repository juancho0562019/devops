
namespace Bext.Reps.Domain.Services
{
    public interface IUserContextReps
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
        string UserName { get; }
        string Email { get; }
        bool EsInterno { get; }
        string Rol { get; }
    }
}