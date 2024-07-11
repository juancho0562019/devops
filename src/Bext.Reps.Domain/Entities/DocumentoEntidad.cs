
using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;

public sealed class DocumentoEntidad : BaseEntity<Guid>
{
    public required DateTime Fecha { get; init; }
    public required string Link { get; init; }
    public required string Nombre { get; init; }
    public required string Descripcion { get; init; }
    public TipoDocumento TipoDocumento { get; init; }
    public required int TipoDocumentoId { get; init; }
    public int EntidadId { get; set; }
    public Entidad? Entidad { get; set; }


    public static DocumentoEntidad Create(DateTime fecha, string link, string nombre, string descripcion, int tipoDocumentoId)
    {
        ArgumentNullException.ThrowIfNull(fecha, nameof(fecha));
        ArgumentException.ThrowIfNullOrEmpty(link, nameof(link));
        ArgumentException.ThrowIfNullOrEmpty(nombre, nameof(nombre));
        ArgumentException.ThrowIfNullOrEmpty(descripcion, nameof(descripcion));
        ArgumentNullException.ThrowIfNull(tipoDocumentoId, nameof(tipoDocumentoId));

        var documento = new DocumentoEntidad
        {
            Fecha = fecha,
            Link = link,
            Nombre = nombre,
            Descripcion = descripcion,
            TipoDocumentoId = tipoDocumentoId
        };

        return documento;
    }
}
