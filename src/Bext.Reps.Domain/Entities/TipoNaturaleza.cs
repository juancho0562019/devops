
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class TipoNaturaleza: BaseEntity<string>
{
    public string Nombre { get; set; } = string.Empty;
    public virtual ICollection<SubTipoNaturaleza> SubTipoNaturalezas { get; set; } = null!;
    public ICollection<CaracterTerritorial>? CaracterTerritorial { get; set; } = null!;
}
