using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class CaracterTerritorial : BaseEntity<string>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string TipoNaturalezaId { get; set; } = string.Empty;
    public TipoNaturaleza TipoNaturaleza { get; set; } = null!;
}
