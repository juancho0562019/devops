using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class EspecificidadPorServicioInscritoSede : BaseEntity<int>
{
    public int EspecificidadId { get; set; }
    public virtual Especificidad Especificidad { get; set; } = null!;
    public int ServicioInscritoSedeId { get; set; }
    public virtual ServicioInscritoSede ServicioInscritoSede { get; set; } = null!;
}
