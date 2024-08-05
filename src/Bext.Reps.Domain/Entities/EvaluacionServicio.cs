using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class EvaluacionServicio : BaseEntity<int>
{
    public TipoEvaluacion TipoEvaluacion { get; set; }
    public int ServicioInscritoSedeId { get; set; }
    public virtual ServicioInscritoSede ServicioInscritoSede { get; set; } = null!;
    public ICollection<DetalleEvaluacionServicio> Detalles { get; set; } = [];

    private EvaluacionServicio(TipoEvaluacion tipoEvaluacion)
    {
        TipoEvaluacion = tipoEvaluacion;
    }

    public static EvaluacionServicio CrearEvaluacionAuto()
    {

        return new EvaluacionServicio(TipoEvaluacion.Auto);
    }

    public static EvaluacionServicio CrearEvaluacionVisita()
    {
        return new EvaluacionServicio(TipoEvaluacion.Visita);
    }

    public void AddDetalle(DetalleEvaluacionServicio detalle)
    {
        detalle.ValidateNull(nameof(detalle));
        Detalles.Add(detalle);
    }
}
