namespace Bext.Reps.Infrastructure.Autenticacion
{
    public class JwtOptions
    {
        public required string IsUser { get; set; } // Issuer
        public required string Audience { get; set; }
        public required string SecretKey { get; set; }
    }
}
