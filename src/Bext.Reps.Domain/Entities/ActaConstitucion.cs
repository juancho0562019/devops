using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities
{
    public class ActaConstitucion : BaseEntity<int>
    {
        public required string CaracterTerritorial { get; set; }
        public required string NivelAtencion { get; set; }
        public required string EmpresaSocialEstado { get; set; }
        public required string ActoConstitucion { get; set; }
        public required string NumeroActo { get; set; }
        public required DateTime FechaActo { get; set; }
        public required string EntidadExpide { get; set; }
        public required string CiudadExpedicion { get; set; }
    }
}
