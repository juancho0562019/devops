using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;

public sealed class Documento : BaseEntity<Guid>
{
    public required DateTime Fecha { get; init; }
    public required string Link { get; init; }
    public required string Nombre { get; init; }
    public required string Descripcion { get; init; }
    public int RegistroModalidadId { get; set; }
    public RegistroModalidad? RegistroModalidad { get; set; }


    public static Documento Create(DateTime fecha, string link, string nombre, string descripcion)
    {
        ArgumentNullException.ThrowIfNull(fecha, nameof(fecha));
        ArgumentException.ThrowIfNullOrEmpty(link, nameof(link));
        ArgumentException.ThrowIfNullOrEmpty(nombre, nameof(nombre));
        ArgumentException.ThrowIfNullOrEmpty(descripcion, nameof(descripcion));

        var documento = new Documento
        {
            Fecha = fecha,
            Link = link,
            Nombre = nombre,
            Descripcion = descripcion
        };

        return documento;
    }
}