
using Microsoft.OpenApi.Models;
using Bext.Reps.Api.Swagger.Filters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

namespace Bext.Reps.Api.Swagger;

public static class ServiceCollectionExtensions
{


    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureSwaggerDocument(this IServiceCollection services)
    {

        services.AddFluentValidationRulesToSwagger();
        services.AddSwaggerGen(c =>
        {

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "[Version1]",
                Title = "[Title] v1",
                Description = "[Description]",
                Contact = new OpenApiContact
                {
                    Name = "[ContactName]",
                    Email = "[ContactEmail]",

                },
            });
            c.OperationFilter<FileOperationFilter>();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Escribir token en el formato: **Bearer {TOKEN}**.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,

            });
            c.OperationFilter<AuthenticationOperationFilter>();

            c.EnableAnnotations();
        });
        return services;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        return services;
    }
}
