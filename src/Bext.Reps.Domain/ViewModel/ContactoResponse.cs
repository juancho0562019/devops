using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Domain.ViewModel
{
    public class ContactoResponse
    {
        public required int Id { get; init; }
        public required TipoContacto TipoContacto { get; init; }
        public required string PrimerNombre { get; init; }
        public string? SegundoNombre { get; init; }
        public required string PrimerApellido { get; init; }
        public string? SegundoApellido { get; init; }
        public required TipoIdentificacion TipoIdentificacion { get; init; }
        public required string NumeroIdentificacion { get; init; }
        public required string Telefono { get; init; }
        public required string CorreoInstitucional { get; init; }
        public string? Profesion { get; init; }
        public string? TarjetaProfesional { get; init; }
    }
}
