using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class DiaAtencion : BaseEntity<int>
{
    public int FranjaHorariaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public FranjaHoraria FranjaHoraria { get; set; } = null!;
}
