using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.ValueObjects;

public sealed class Ubicacion : ValueObject
{
    public string Pais { get; set; } = string.Empty;
    public string Departamento { get; private set; } = null!;
    public string Municipio { get; private set; } = null!;
    public string Direccion { get; set; } = string.Empty;
    public static Ubicacion Crear(string pais, string departamento, string municipio)
    {
        ArgumentException.ThrowIfNullOrEmpty(pais);
        ArgumentException.ThrowIfNullOrEmpty(departamento);
        ArgumentException.ThrowIfNullOrEmpty(municipio);

        return new Ubicacion
        {
            Pais = pais,
            Departamento = departamento,
            Municipio = municipio
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Pais;
        yield return Departamento;
        yield return Municipio;
    }

}
