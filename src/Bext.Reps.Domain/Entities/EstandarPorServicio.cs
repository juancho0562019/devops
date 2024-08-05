using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class EstandarPorServicio : BaseEntity<int>
{
    public int EstandarId { get; set; }
    public virtual Estandar Estandar { get; set; } = null!;
    public int ServicioId { get; set; }
    public virtual Servicio Servicio { get; set; } = null!;
}
