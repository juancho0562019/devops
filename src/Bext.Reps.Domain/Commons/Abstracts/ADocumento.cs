using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Commons.Abstracts;
public abstract class ADocumento<TKey> : BaseEntity<TKey> where TKey : notnull
{
    public required DateTime Fecha { get; init; }
    public required string Nombre { get; init; }
    public required string Descripcion { get; init; }
    public required EstadoDocumento EstadoDocumento { get; set; }
}
