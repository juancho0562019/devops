using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;

public sealed class Nombre : ValueObject
{
    public required string PrimerNombre { get; init; }

    public required string PrimerApellido { get; init; }

    public string? SegundoNombre { get; init; }

    public string? SegundoApellido { get; init; }

    public static Nombre Crear(string primerNombre, string primerApellido, string? segundoNombre = null, string? segundoApellido = null)
    {
        ArgumentNullException.ThrowIfNull(primerNombre);
        ArgumentNullException.ThrowIfNull(primerApellido);

        var nombre = new Nombre
        {
            PrimerNombre = primerNombre,
            PrimerApellido = primerApellido,
            SegundoNombre = segundoNombre,
            SegundoApellido = segundoApellido
        };

        return nombre;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return PrimerNombre;
        yield return PrimerApellido;
        yield return SegundoNombre ?? "";
        yield return SegundoApellido ?? "";
    }
}
