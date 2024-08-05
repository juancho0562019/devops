
using Bext.Reps.Domain.Commons.Abstracts;

namespace Bext.Reps.Domain.Entities;

public sealed class DocumentoEntidad : ADocumento<Guid>
{
    public TipoDocumento TipoDocumento { get; init; } = null!;
    public required int TipoDocumentoId { get; init; }
    public int EntidadId { get; set; }
    public Entidad? Entidad { get; set; }

    public static DocumentoEntidad Create(DateTime fecha, string link, string? nombre, string? descripcion, int? tipoDocumentoId)
    {
        ArgumentNullException.ThrowIfNull(fecha);
        ArgumentException.ThrowIfNullOrEmpty(nombre);
        ArgumentException.ThrowIfNullOrEmpty(descripcion);
        ArgumentNullException.ThrowIfNull(tipoDocumentoId);

        var documento = new DocumentoEntidad
        {
            Fecha = fecha,
            Nombre = nombre,
            Descripcion = descripcion,
            TipoDocumentoId = (int)tipoDocumentoId,
            EstadoDocumento = Commons.Enums.EstadoDocumento.Recibido
        };

        return documento;
    }
}
