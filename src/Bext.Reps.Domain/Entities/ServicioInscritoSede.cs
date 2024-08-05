using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class ServicioInscritoSede : BaseEntity<int>
{
    public int SedeId { get; private set; }
    public int GrupoServicioId { get; private set; }
    public int ServicioId { get; private set; }
    public int SolicitudId { get; set; }
    public int ComplejidadServicioId { get; set; }
    public Solicitud Solicitud { get; set; } = null!;
    public ICollection<EvaluacionServicio> Evaluaciones { get; set; } = [];
    public ICollection<CapacidadInstalada> CapacidadesInstaladas { get; set; } = [];
    public ICollection<FranjaHoraria> FranjasHorarias { get; set; } = [];
    public static ServicioInscritoSede Create(int sedeId, int grupoServicioId, int servicioId, List<CapacidadInstalada > capacidad, EvaluacionServicio evaluacion, IEnumerable<FranjaHoraria> franjasHorarias)
    {
        var servicio = new ServicioInscritoSede
        {
            SedeId = sedeId,
            GrupoServicioId = grupoServicioId,
            ServicioId = servicioId
        };
        servicio.AddEvaluacion(evaluacion);
        foreach (var item in capacidad)
        {
            servicio.AddCapacidadInstalada(item);
        }
        

        foreach (var franjaHoraria in franjasHorarias)
        {
            servicio.AddFranjaHoraria(franjaHoraria);
        }

        return servicio;
    }
    public void AddEvaluacion(EvaluacionServicio evaluacion)
    {
        evaluacion.ValidateNull(parameterName: nameof(evaluacion));
        Evaluaciones.Add(evaluacion);
    }

    public void AddCapacidadInstalada(CapacidadInstalada nuevaCapacidad)
    {
        nuevaCapacidad.ValidateNull(parameterName: nameof(nuevaCapacidad));

        var capacidadesActuales = CapacidadesInstaladas
            .Where(c => c.TipoRecurso == nuevaCapacidad.TipoRecurso && c.Activo)
            .ToList();

        foreach (var capacidad in capacidadesActuales)
        {
            capacidad.Desactivar();
        }

        CapacidadesInstaladas.Add(nuevaCapacidad);
    }

    public void AddFranjaHoraria(FranjaHoraria nuevaFranjaHoraria)
    {
        nuevaFranjaHoraria.ValidateNull(parameterName: nameof(nuevaFranjaHoraria));

        FranjasHorarias.Add(nuevaFranjaHoraria);
    }
}
