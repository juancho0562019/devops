using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Commons.ValueObjects;

namespace Bext.Reps.Domain.Commons.Abstracts;
public abstract class Persona<TId> : BaseEntity<TId> where TId : notnull
{
    public Identificacion Identificacion { get; protected set; } = null!;
    public Nombre Nombre { get; protected set; } = null!;
    public Ubicacion Ubicacion { get; protected set; } = null!;
    public Contacto DatosContacto { get; protected set; } = null!;
}
