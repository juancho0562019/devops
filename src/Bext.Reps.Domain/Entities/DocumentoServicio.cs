using Bext.Reps.Domain.Commons.Abstracts;
using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Domain.Entities;

public sealed class DocumentoServicio : ADocumento<Guid>
{
    public int SolicitudId { get; set; }
    public Solicitud Solicitud { get; init; } = null!;
    public TipoDocumento TipoDocumento { get; init; } = null!;
    public required int TipoDocumentoId { get; init; }
    public static DocumentoServicio Create(DateTime fecha, string link, string nombre, string? descripcion, int? tipoDocumentoId)
    {
        
        var documento = new DocumentoServicio
        {
            Fecha = fecha.ValidateNull(parameterName: nameof(fecha)),
            Nombre = nombre.ValidateNotNullOrEmpty(parameterName: nameof(nombre)),
            Descripcion = descripcion.ValidateNotNullOrEmpty(parameterName: nameof(descripcion)),
            TipoDocumentoId = tipoDocumentoId.ValidateNull(parameterName: nameof(tipoDocumentoId)) ?? 0,
            EstadoDocumento = EstadoDocumento.Recibido
        };

        return documento;
    }
  
}
