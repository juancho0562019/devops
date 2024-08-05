using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Commons.ValueObjects;

public sealed class Ubicacion : ValueObject
{
    //public string Pais { get; set; } = string.Empty;
    public string Departamento { get; private set; } = null!;
    public string Municipio { get; private set; } = null!;
    public string Direccion { get; set; } = string.Empty;
    public static Ubicacion Crear(string departamento, string municipio, string direccion)
    {

        ArgumentException.ThrowIfNullOrEmpty(departamento);
        ArgumentException.ThrowIfNullOrEmpty(municipio);
        ArgumentException.ThrowIfNullOrEmpty(direccion);

        return new Ubicacion
        {

            Departamento = departamento,
            Municipio = municipio,
            Direccion = direccion
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
      
        yield return Departamento;
        yield return Municipio;
    }

}
