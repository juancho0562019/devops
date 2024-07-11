using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.ViewModel;

namespace Bext.Reps.Business.Abstractions
{
    public interface IJwtProvider
    {
        (string, RefreshToken) Generate(LoginResponseReps loginResponse);
    }
}
