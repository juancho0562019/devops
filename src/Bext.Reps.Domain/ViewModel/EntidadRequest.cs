
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Domain.ViewModel
{
    public sealed class EntidadRequest
    {
        public required string TipoNaturalezaJuridica { get; init; }
        public required string TipoEntidad { get; init; }
        public required string TipoIdentificacion { get; init; }
        public required string NumeroIdentificacion { get; init; }
        public required string Nombre { get; init; }
        public string? Sigla { get; init; }
        public DateTime FechaUltimaActualizacion { get; init; }
        public required string ActividadEconomica { get; init; }
        public required string CorreoElectronico { get; init; }
        public required string TelefonoPrincipal { get; init; }
        public string? TelefonoAdicional { get; init; }
        public required string DireccionNotificacionJudicial { get; init; }
        public required string DireccionEstablecimiento { get; init; }
        public bool AceptaNotificacionesCorreoElectronico { get; init; }
        public bool AceptaTerminosYCondiciones { get; init; }
        public required string Pais { get; init; }
        public required string Departamento { get; init; }
        public required string Municipio { get; init; }
    }
}
