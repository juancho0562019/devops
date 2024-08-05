using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class SubTipoNaturaleza: BaseEntity<string>
{
    public string Nombre { get; set; } = string.Empty;
    public string TipoNaturalezaId { get; set; } = string.Empty;
    public virtual TipoNaturaleza TipoNaturaleza { get; set; } = null!;

    public ICollection<DocumentoConstitucion> DocumentosConstitucion { get; set; } = null!;
}
