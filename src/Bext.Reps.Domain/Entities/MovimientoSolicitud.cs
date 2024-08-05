using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class MovimientoSolicitud : BaseEntity<int>
{
    public int SolicitudId { get; set; }
    public Solicitud Solicitud { get; set; } = null!;
    public EstadoSolicitud EstadoSolicitud { get; set; }
    public string Accion { get; set; } = string.Empty;

}
