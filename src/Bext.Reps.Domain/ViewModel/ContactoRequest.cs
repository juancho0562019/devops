using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Domain.ViewModel
{
    public class ContactoRequest
    {
        public required TipoContacto TipoContacto { get; init; }
        public required string PrimerNombre { get; init; }
        public string? SegundoNombre { get; init; }
        public required string PrimerApellido { get; init; }
        public string? SegundoApellido { get; init; }
        public required string TipoIdentificacion { get; init; }
        public required string NumeroIdentificacion { get; init; }
        public required string Telefono { get; init; }
        public required string CorreoInstitucional { get; init; }
        public string? Profesion { get; init; }
        public string? TarjetaProfesional { get; init; }
        public string? InformacionOficio { get; init; }
        public DateTime? FechaDocumentoAutorizacion { get; init; }
        public TipoRepresentanteLegal? TipoRepresentanteLegal { get; init; }
    }
}
