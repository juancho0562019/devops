using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class CapacidadInstalada : BaseEntity<int>
{
    public int ServicioInscritoSedeId { get; set; }
    public virtual ServicioInscritoSede ServicioInscritoSede { get; set; } = null!;
    public TipoRecurso TipoRecurso { get; set; }
    public int Capacidad { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool Activo { get; set; }

    public static CapacidadInstalada Create(TipoRecurso tipoRecurso, int capacidad)
    {
        return new CapacidadInstalada
        {
            TipoRecurso = tipoRecurso,
            Capacidad = capacidad,
            FechaInicio = DateTime.UtcNow,
            Activo = true
        };
    }

    public void Desactivar()
    {
        this.Activo = false;
        this.FechaFin = DateTime.Now;
    }
}
