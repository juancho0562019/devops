using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;
public class TipoPersona: BaseEntity<string>
{
    public string Nombre { get; set; }
}
