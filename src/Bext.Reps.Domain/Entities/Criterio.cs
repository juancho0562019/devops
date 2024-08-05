using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class Criterio : BaseEntity<int>
{
    public string Nombre { get; set; } = string.Empty;
    public int EstandarId { get; set; }
    public virtual Estandar Estandar { get; set; } = null!;
}
