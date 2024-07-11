using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities
{
    public class TipoDocumento : BaseEntity<int>
    {
        public required string Nombre { get; set; }
    }
}
