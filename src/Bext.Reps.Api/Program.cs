using System.Text.Json.Serialization;
using Bext.Reps.Api;
using Bext.Reps.Api.Middleware;
using Bext.Reps.Api.Swagger;
using Bext.Reps.Business;
using Bext.Reps.Domain.Entities;
using Bext.Reps.Infrastructure;
using Bext.Reps.Infrastructure.Data;
using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddMemoryCache();
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });


builder.Services.AddBussines();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

builder.Services.AddCustomCors();



builder.Services.AddHttpContextAccessor();


builder.Logging.AddSerilog(new LoggerConfiguration()
       .ReadFrom.Configuration(builder.Configuration)
          .CreateLogger());




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//    });
//    options.OperationFilter<SecurityRequirementsOperationFilter>();
//});
builder.Services.ConfigureSwaggerDocument();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

var app = builder.Build();
//app.UseCustomConfigure();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();


app.ConfigureCors();

app.UseAuthorization() ;
app.ConfigureCors();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
