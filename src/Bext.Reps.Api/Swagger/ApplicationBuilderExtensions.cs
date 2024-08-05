using Microsoft.Extensions.Configuration;

namespace Bext.Reps.Api.Swagger;

public static class ApplicationBuilderExtensions
{


    public static IApplicationBuilder ConfigureSwaggerUI(this IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reps.API");
        });

        return app;
    }

    public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
    {
        app.UseCors("CorsPolicy");
        return app;
    }
}
