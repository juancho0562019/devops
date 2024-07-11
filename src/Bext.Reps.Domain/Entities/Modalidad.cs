using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;

public sealed class Modalidad : BaseEntity<int>
{
    public required string Descripcion { get; init; }

    public List<RegistroModalidad> RegistrosModalidad { get; private set; } = [];

    public static Modalidad Crear(string descripcion)
    {
        ArgumentException.ThrowIfNullOrEmpty(descripcion, nameof(descripcion));

        var modalidad = new Modalidad
        {
            Descripcion = descripcion
        };

        return modalidad;
    }

}