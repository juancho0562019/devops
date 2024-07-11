using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Domain.ViewModel
{
    public sealed class EntidadResponse
    {
        public required int Id { get; init; }
        public required TipoNaturalezaJuridica TipoNaturalezaJuridica { get; init; }
        public required TipoEntidad TipoEntidad { get; init; }
        public required TipoIdentificacion TipoIdentificacion { get; init; }
        public required string NumeroIdentificacion { get; init; }
        public required string DigitoVerificacion { get; init; }
        public required string Nombre { get; init; }
        public string? Sigla { get; init; }
        public DateTime FechaUltimaActualizacion { get; init; }
        public required string Pais { get; init; }
        public required string Departamento { get; init; }
        public required string Municipio { get; init; }
        public List<TerceroResponse> Terceros { get; init; } = [];
        public List<ContactoResponse> Contactos { get; init; } = [];
    }
}
