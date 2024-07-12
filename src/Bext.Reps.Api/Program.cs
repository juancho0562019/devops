using Bext.Reps.Api;
using Bext.Reps.Api.Middleware;
using Bext.Reps.Api.OptionsSetup;
using Bext.Reps.Business;
using Bext.Reps.Business.Data;
using Bext.Reps.Domain.Services;
using Bext.Reps.Infrastructure.Data;
using Bext.Reps.Infrastructure.Services.Users;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Serilog;

using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddBussines();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Registro del Logger
builder.Logging.AddSerilog(new LoggerConfiguration()
       .ReadFrom.Configuration(builder.Configuration)
          .CreateLogger());

var connectionString = builder.Configuration.GetConnectionString("RepsConnectionString");
ArgumentException.ThrowIfNullOrEmpty(connectionString, "RepsConnectionString");

// Add services to the container.
builder.Services.AddDbContext<RepsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<IRepsDbContext, RepsDbContext>();

builder.Services.RegistrarConsultasYComandos();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
