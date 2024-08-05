using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class Estandar : BaseEntity<int>
{
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Criterio> Criterios { get; set; } = [];
}
