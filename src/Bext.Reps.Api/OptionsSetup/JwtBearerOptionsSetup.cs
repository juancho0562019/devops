using Bext.Reps.Infrastructure.Autenticacion;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bext.Reps.Api.OptionsSetup
{
    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }
        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.IsUser,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };

        }
    }
}
