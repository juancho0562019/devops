using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities
{
    public sealed class RefreshToken : ValueObject
    {
        public string Token { get; private set; } = null!;
        public DateTime FechaCreacion { get; private set; } = DateTime.Now;
        public DateTime Expiracion { get; private set; } = DateTime.Now.AddDays(30);

        public static RefreshToken Crear(string token, DateTime expiration)
        {
            ArgumentException.ThrowIfNullOrEmpty(token, nameof(token));
            ArgumentNullException.ThrowIfNull(expiration, nameof(expiration));

            return new RefreshToken() { Token = token, Expiracion = expiration };
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return FechaCreacion;
            yield return Expiracion;
        }
    }
}
