using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class GrupoServicio : BaseEntity<int>
{
    public int ModalidadId { get; set; }
    public virtual Modalidad Modalidad { get; set; } = null!;

    public string Nombre { get; set; } = string.Empty;

    public ICollection<Servicio> Servicios { get; set; } = null!;
}
