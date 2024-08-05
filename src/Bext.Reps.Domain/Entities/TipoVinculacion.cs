using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class TipoVinculacion : BaseEntity<string>
{
    public string Nombre { get; set; } = string.Empty;
}
