
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class FranjaHoraria : BaseEntity<int>
{
    public int ServicioInscritoSedeId { get; set; }
    public TimeSpan HoraApertura { get; set; }
    public TimeSpan HoraCierre { get; set; }
    public virtual ServicioInscritoSede ServicioInscritoSede { get; set; } = null!;
    public ICollection<DiaAtencion> DiasAtencion { get; set; } = new List<DiaAtencion>();

    public static FranjaHoraria Create(TimeSpan horaApertura, TimeSpan horaCierre, IEnumerable<DayOfWeek> diasAtencion)
    {
        if (horaApertura >= horaCierre)
        {
            throw new ArgumentException("La hora de apertura debe ser anterior a la hora de cierre.");
        }

        var franjaHoraria = new FranjaHoraria
        {
            HoraApertura = horaApertura,
            HoraCierre = horaCierre
        };

        foreach (var dia in diasAtencion)
        {
            franjaHoraria.DiasAtencion.Add(new DiaAtencion { DiaSemana = dia });
        }

        return franjaHoraria;
    }
}
