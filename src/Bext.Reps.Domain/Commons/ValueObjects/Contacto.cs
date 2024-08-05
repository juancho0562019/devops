using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Commons.ValueObjects;

public sealed class Contacto : ValueObject
{
    public string? TelefonoFijo { get; private set; }
    public string? TelefonoMovil { get; private set; }
    public string? TelefonoFax { get; private set; }
    public string? SitioWeb { get; private set; }
    public string? Email { get; private set; }

    public static Contacto Crear(string? telefonoFijo, string? telefonoMovil, string? telefonoFax, string? sitioWeb, string? email)
    {
        var contacto = new Contacto
        {
            TelefonoFijo = telefonoFijo,
            TelefonoMovil = telefonoMovil,
            TelefonoFax = telefonoFax,
            SitioWeb = sitioWeb,
            Email = email
        };
        contacto.Validate();
        return contacto;
    }
    private void Validate()
    {
        if (string.IsNullOrEmpty(Email))
        {
            throw new ArgumentException("El email debe ser una dirección válida.");
        }

        if (!string.IsNullOrEmpty(SitioWeb) && !Uri.IsWellFormedUriString(SitioWeb, UriKind.RelativeOrAbsolute))
        {
            throw new ArgumentException("El sitio web debe ser una URL válida.");
        }
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return TelefonoFijo ?? "";
        yield return TelefonoMovil ?? "";
        yield return TelefonoFax ?? "";
        yield return SitioWeb ?? "";
        yield return Email ?? "";
    }
}
