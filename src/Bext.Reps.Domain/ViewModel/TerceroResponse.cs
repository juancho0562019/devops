using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Domain.ViewModel
{
    public class TerceroResponse
    {
        public required int Id { get; init; }
        public required TipoIdentificacion TipoIdentificacion { get; init; }
        public required string NumeroIdentificacion { get; init; }
        public required string Nombre { get; init; }
        public required string Direccion { get; init; }
        public required string Telefono { get; init; }
        public required string Email { get; init; }
    }
}
