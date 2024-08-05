using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class Complejidad : BaseEntity<int>
{
    public string Nivel { get; set; } = string.Empty;
    
}
