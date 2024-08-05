using System.Text.RegularExpressions;
using Bext.Reps.Domain.Commons.Primitives;
namespace Bext.Reps.Domain.Commons.ValueObjects;

public sealed class Identificacion : ValueObject
{
    public required string TipoIdentificacion { get; init; }
    public required string NumeroDocumento { get; init; }
    public short DigitoVerificacion { get; private set; }

    public static Identificacion Crear(string tipoIdentificacion, string numeroDocumento, bool generarDigitoVerificacion = false)
    {

        ArgumentException.ThrowIfNullOrEmpty(numeroDocumento);
        ArgumentException.ThrowIfNullOrEmpty(tipoIdentificacion);

        var digitoVerificacion = generarDigitoVerificacion ? CalcularDigitoVerificacion(numeroDocumento) : (short)0;

        return new Identificacion
        {
            TipoIdentificacion = tipoIdentificacion,
            NumeroDocumento = numeroDocumento,
            DigitoVerificacion = digitoVerificacion
        };
    }

  
    public static short CalcularDigitoVerificacion(string nit)
    {
        try
        {
            nit = Regex.Replace(nit, "[^0-9]", "");

            if (long.TryParse(nit, out long lNit))
            {
                var vpri = new int[16];
                vpri[1] = 3;
                vpri[2] = 7;
                vpri[3] = 13;
                vpri[4] = 17;
                vpri[5] = 19;
                vpri[6] = 23;
                vpri[7] = 29;
                vpri[8] = 37;
                vpri[9] = 41;
                vpri[10] = 43;
                vpri[11] = 47;
                vpri[12] = 53;
                vpri[13] = 59;
                vpri[14] = 67;
                vpri[15] = 71;

                int z = nit.Length;

                int x = 0;
                int y = 0;

                for (var i = 0; i < z; i++)
                {
                    y = int.Parse(nit.Substring(i, 1));

                    x += (y * vpri[z - i]);
                }

                y = x % 11;

                return (short)((y > 1) ? 11 - y : y);
            }
            else
            {
                return 0;
            }
        }
        catch
        {
            return 0;
        }
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return TipoIdentificacion;
        yield return NumeroDocumento;
        yield return DigitoVerificacion;
    }
}
