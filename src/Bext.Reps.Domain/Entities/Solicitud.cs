using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class Solicitud : AggregateRoot<int>
{
    public Solicitud(int id) : base(id)
    {
    }
    public int EntidadId { get; set; }
    public virtual Entidad Entidad { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public TipoSolicitud TipoSolicitud { get; set; }
    public EstadoSolicitud EstadoSolicitud { get; set; }
    public ICollection<MovimientoSolicitud> MovimientosSolicitud { get; set; } = [];
    public ICollection<ServicioInscritoSede> Servicios { get; set; } = [];
    public ICollection<DocumentoServicio> Documentos { get; set; } = [];
    public void AddServicio(ServicioInscritoSede servicio)
    {
        servicio.ValidateNull(parameterName: nameof(servicio));
        Servicios.Add(servicio);
    }

    public void AddDocumentoServicio(DocumentoServicio documento)
    {
        documento.ValidateNull(parameterName: nameof(documento));
        Documentos.Add(documento);
    }
    private Solicitud(int id, int entidadId, TipoSolicitud tipoSolicitud, DateTime fecha) : base(id)
    {
        EntidadId = entidadId;
        TipoSolicitud = tipoSolicitud;
        Fecha = fecha;
        EstadoSolicitud = EstadoSolicitud.Radicada;
    }
    public static Solicitud CrearSolicitudHabilitacion(int id, int entidadId, DateTime fecha)
    {
        return new Solicitud(id, entidadId, TipoSolicitud.Habilitacion, fecha);
    }

  
    public static Solicitud CrearSolicitudNovedad(int id, int entidadId, DateTime fecha)
    {
        return new Solicitud(id, entidadId, TipoSolicitud.Novedad, fecha);
    }

}
