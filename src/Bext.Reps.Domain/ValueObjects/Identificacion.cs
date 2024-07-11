
using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.ValueObjects;

public sealed class Identificacion : ValueObject
{
    public required string TipoIdentificacionId { get; init; }
    public DocumentoIdentidad TipoIdentificacion { get; init; } = null!;

    public required string NumeroDocumento { get; init; }

    public short DigitoVerificacion { get; private set; }

    public static Identificacion Crear(string tipoIdentificacion, string numeroDocumento, bool generarDigitoVerificacion = false)
    {

        ArgumentException.ThrowIfNullOrEmpty(numeroDocumento);
        ArgumentException.ThrowIfNullOrEmpty(tipoIdentificacion);

        var digitoVerificacion = generarDigitoVerificacion ? CalcularDigitoVerificacion(numeroDocumento) : (short)0;

        return new Identificacion
        {
            TipoIdentificacionId = tipoIdentificacion,
            NumeroDocumento = numeroDocumento,
            DigitoVerificacion = digitoVerificacion
        };
    }

    public static short CalcularDigitoVerificacion(string nit)
    {
        ArgumentException.ThrowIfNullOrEmpty(nit, nameof(nit));

        int[] array = { 41, 37, 29, 23, 19, 17, 13, 7, 3 };
        int suma = 0;
        for (int i = 0; i < nit.Length; i++)
        {
            int pos = nit.Length - i;
            suma += int.Parse(nit.Substring(i, 1)) * array[pos - 1];
        }
        int modulo = suma % 11;

        return modulo == 0 || modulo == 1 ? (short)modulo : (short)(11 - modulo);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return TipoIdentificacionId;
        yield return NumeroDocumento;
        yield return DigitoVerificacion;
    }
}
