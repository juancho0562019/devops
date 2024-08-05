using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Commons.ValueObjects;

namespace Bext.Reps.Domain.Entities;
public sealed class ContactoEntidad : BaseEntity<int>
{
    public required Nombre Nombre { get; init; }
    public required Identificacion Identificacion { get; init; }
    public string? TipoVinculacionId { get; set; }
    public TipoVinculacion? TipoVinculacion { get; set; } = null!;
    public ICollection<PeriodoRepresentacion>? PeriodosRepresentacion { get; set; }

    public static ContactoEntidad Crear(Nombre nombre, Identificacion identificacion)
    {
        nombre.ValidateNull("Nombres");
        identificacion.ValidateNull("Identificacion");

        var contacto = new ContactoEntidad
        {
            Nombre = nombre,
            Identificacion = identificacion,
        };

        return contacto;
    }

}
