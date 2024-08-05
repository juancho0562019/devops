using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class TipoPersona: BaseEntity<string>
{
    public string Nombre { get; set; } = string.Empty;
    public bool RequiereRepresentante { get; set; }
}
