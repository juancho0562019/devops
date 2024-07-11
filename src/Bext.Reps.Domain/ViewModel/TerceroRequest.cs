
namespace Bext.Reps.Domain.ViewModel
{
    public class TerceroRequest
    {
        public required string TipoIdentificacion { get; init; }
        public required string NumeroIdentificacion { get; init; }
        public required string Nombre { get; init; }
        public required string Direccion { get; init; }
        public required string Telefono { get; init; }
        public required string Email { get; init; }
    }
}
