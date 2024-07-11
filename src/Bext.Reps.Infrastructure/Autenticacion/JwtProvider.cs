using Bext.Reps.Business.Abstractions;
using Bext.Reps.Domain.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Bext.Reps.Domain.Entities;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Bext.Reps.Infrastructure.Autenticacion
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor = null!;

        public JwtProvider(IOptions<JwtOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public (string, RefreshToken) Generate(LoginResponseReps loginResponse)
        {
            var claims = SetClaims(loginResponse);


            var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                                            SecurityAlgorithms.HmacSha256);

            var tokenString = GetAccessToken(claims, signingCredentials);
            var refreshToken = GetRefreshToken();
            SetRefreshToken(refreshToken);

            return (tokenString, refreshToken);
        }

        private Claim[] SetClaims(LoginResponseReps loginResponse)
        {
            return
            [
                new Claim(JwtRegisteredClaimNames.Sub, loginResponse.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, loginResponse.Nombre),
                new Claim(JwtRegisteredClaimNames.Email, loginResponse.Correo),
                new Claim(ClaimTypes.Role, loginResponse.Rol),
                new Claim("EsInterno", loginResponse.EsInterno.ToString())
            ];
        }

        private string GetAccessToken(Claim[] claims, SigningCredentials signingCredentials)
        {
            var token = new JwtSecurityToken(
                _options.IsUser,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        private RefreshToken GetRefreshToken()
        {
            var refreshToken = RefreshToken.Crear(
                               Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                               DateTime.Now.AddDays(30));

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            // Guardar refreshToken en la base de datos
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expiracion,
                Secure = true
            };

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
