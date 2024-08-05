namespace Bext.Reps.Domain.Commons.Primitives;
public abstract class BaseAuditableEntity
{
 
    public DateTimeOffset Creacion { get; set; }

    public string? CreadoPor { get; set; }

    public DateTimeOffset UltimaModificacion { get; set; }

    public string? UltimaModificacionPor { get; set; }
    public bool EstadoRegistro { get; set; }
}
