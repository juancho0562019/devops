using Bext.Reps.Domain.Commons.Abstracts;
using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Domain.Entities;

public sealed class DocumentoSede : ADocumento<Guid>
{
    public TipoDocumento TipoDocumento { get; init; } = null!;
    public  int TipoDocumentoId { get; init; }
    public  int SedeId { get; set; }
    public Sede Sede { get; init; } = null!;
    public static DocumentoSede Create(DateTime fecha,string nombre, string descripcion, int tipoDocumentoId)
    {
        
        var documento = new DocumentoSede
        {
          
            Fecha = fecha.ValidateNull(parameterName: nameof(fecha)),
            Nombre = nombre.ValidateNotNullOrEmpty(parameterName: nameof(nombre)),
            Descripcion = descripcion.ValidateNotNullOrEmpty(parameterName: nameof(descripcion)),
            TipoDocumentoId = tipoDocumentoId,
            EstadoDocumento = EstadoDocumento.Recibido
        };

        return documento;
    }
}
