using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities
{
    public class DocumentoIdentidad : BaseEntity<string>
    {
        public string Nombre { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
    }
}
