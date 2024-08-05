using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Commons.ValueObjects;

public sealed class Nombre : ValueObject
{
    public string? PrimerNombre { get; private set; }
    public string? PrimerApellido { get; private set; }
    public string? SegundoNombre { get; private set; }
    public string? SegundoApellido { get; private set; }
    public string? RazonSocial { get; private set; }

    private Nombre() { }

    public static Nombre Crear(string? razonSocial, string? primerNombre, string? primerApellido, string? segundoNombre = null, string? segundoApellido = null)
    {
        var nombre = new Nombre
        {
            PrimerNombre = primerNombre,
            PrimerApellido = primerApellido,
            SegundoNombre = segundoNombre,
            SegundoApellido = segundoApellido,
            RazonSocial = razonSocial
        };    
        nombre.Validate();
        return nombre;

    }
    private void Validate()
    {
        if (string.IsNullOrEmpty(RazonSocial) && (string.IsNullOrEmpty(PrimerNombre) || string.IsNullOrEmpty(PrimerApellido)))
        {
            throw new ArgumentException("Si RazonSocial está vacía, PrimerNombre y PrimerApellido deben estar presentes.");
        }

        //if (!string.IsNullOrEmpty(RazonSocial) && (!string.IsNullOrEmpty(PrimerNombre) || !string.IsNullOrEmpty(PrimerApellido)))
        //{
        //    throw new ArgumentException("Si RazonSocial está presente, PrimerNombre y PrimerApellido deben estar vacíos.");
        //}
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return PrimerNombre ?? "";
        yield return PrimerApellido ?? "";
        yield return SegundoNombre ?? "";
        yield return SegundoApellido ?? "";
        yield return RazonSocial ?? "";
    }
}
