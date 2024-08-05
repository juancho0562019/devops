using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class Modalidad : BaseEntity<int>
{
    public string Nombre { get; set; } = string.Empty;
}
