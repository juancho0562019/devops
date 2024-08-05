using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class DetalleEvaluacionServicio : BaseEntity<int>
{
    public int EstandarId { get; set; }
    public int CriterioId { get; set; }
    public bool Cumple { get; set; }
    public virtual Estandar Estandar { get; set; } = null!;
    public virtual Criterio Criterio { get; set; } = null!;


}
