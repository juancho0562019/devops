using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class NivelAtencion : BaseEntity<int>
{
    public int Nivel { get; set; }
    public string Nombre { get; set; } = string.Empty;
    
}
