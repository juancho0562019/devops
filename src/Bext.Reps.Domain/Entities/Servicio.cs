using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class Servicio : BaseEntity<int>
{
    public string Nombre { get; set; } = string.Empty;
    public int GrupoServicioId { get; set; }
    public GrupoServicio GrupoServicio { get; set; } = null!;
    public ICollection<EstandarPorServicio> Estandares { get; set; } = [];
    public ICollection<EspecificidadPorServicioInscritoSede> Especificidades { get; set; } = [];
}
