using Bext.Reps.Business.Abstractions;
using Bext.Reps.Domain.Services;
using Bext.Reps.Infrastructure.Autenticacion;
using Bext.Reps.Infrastructure.Services.Users;


namespace Bext.Reps.Api;

public static class RegistroContenedor
{
    public static IServiceCollection RegistrarConsultasYComandos(this IServiceCollection serviceCollection)
    {
        // Entidades - Contactos y Terceros
        serviceCollection.AddScoped<IUserContextReps, UserContextReps>();
        
        serviceCollection.AddTransient<IJwtProvider, JwtProvider>();

        return serviceCollection;
    }
}
