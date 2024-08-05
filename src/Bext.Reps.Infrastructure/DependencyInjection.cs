using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Infrastructure.Data;
using Bext.Reps.Infrastructure.Data.Repository;
using Bext.Reps.Infrastructure.Services;
using Bext.Reps.Infrastructure.Services.Rest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bext.Reps.Infrastructure;
public static class DependencyInjection
{
 

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RepsConnectionString");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        services.AddDbContext<RepsDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(typeof(RepsDbContext).Assembly.FullName)));

        services.AddScoped<IRepsDbContext>(provider => provider.GetService<RepsDbContext>());
        services.AddScoped<RepsDbContextInitialiser>();
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
        services.AddScoped<ITerceroRepository, TerceroRepository>();
        services.AddScoped<IEntidadRepository, EntidadRepository>();
        services.AddScoped<ISedeRepository, SedeRepository>();

        services.AddScoped<IDocumentoService, DocumentoService>();

      
        services.AddHealthChecks().AddSqlServer(connectionString);

        var clientOptions = configuration.GetSection("ClientOptions").Get<ClientOptions>();

        ArgumentNullException.ThrowIfNull(clientOptions);

        var globalValidFile = configuration.GetSection("GlobalValidFile");

        ArgumentNullException.ThrowIfNull(globalValidFile);
        
        services.Configure<GlobalValidFile>(globalValidFile);

        services.AddSingleton<IRestDirectorioGeneralClientFactory>(provider =>
            new RestDirectorioGeneralClientFactory(clientOptions));

        services.AddTransient<IDepartamentoClient>(provider =>
            provider.GetRequiredService<IRestDirectorioGeneralClientFactory>().CreateDepartamentoClient());
        
        services.AddTransient<IMunicipioClient>(provider =>
            provider.GetRequiredService<IRestDirectorioGeneralClientFactory>().CreateMunicipioClient());

        var controlDocOptions = configuration.GetSection("ClientOptionsControlDoc").Get<ClientOptions>();

        ArgumentNullException.ThrowIfNull(controlDocOptions);

        services.AddScoped<IControlDocClient>(provider => new ControlDocClient(controlDocOptions));

        services.AddTransient<IDirectorioGeneralRepository, DirectorioGeneralRepository>();
        return services;
    }
}
