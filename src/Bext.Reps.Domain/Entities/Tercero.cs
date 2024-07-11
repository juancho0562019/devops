using Bext.Reps.Domain.Primitives;
using Bext.Reps.Domain.ValueObjects;

namespace Bext.Reps.Domain.Entities;
public sealed class Tercero : BaseEntity<int>
{
    public TipoPersona TipoPersona { get; set; }
    public string TipoPersonaId { get; set; }
    public Identificacion Identificacion { get; private set; }
    public string? PrimerNombre { get; private set; }
    public string? SegundoNombre { get; private set; }
    public string? PrimerApellido { get; private set; }
    public string? SegundoApellido { get; private set; }
    public string? RazonSocial { get; private set; }
    public Ubicacion Ubicacion { get; private set; }
    public string? TelefonoFijo { get; private set; }
    public string? TelefonoMovil { get; private set; }
    public string? TelefonoFax { get; private set; }
    public string? SitioWeb { get; private set; }
    public string? Email { get; private set; }

    private Tercero() { }


    public void ActualizarTipoPersona(TipoPersona tipoPersona)
    {
        TipoPersona = tipoPersona.ValidateNull(nameof(tipoPersona));
    }
    public void ActualizarIdentificacion(Identificacion identificacion)
    {
        Identificacion = identificacion.ValidateNull(nameof(identificacion));
    }

    public void ActualizarNombres(string? primerNombre, string? segundoNombre, string? primerApellido, string? segundoApellido)
    {
        PrimerNombre = primerNombre.ValidateNotNullOrEmpty(nameof(primerNombre));
        SegundoNombre = segundoNombre;
        PrimerApellido = primerApellido.ValidateNotNullOrEmpty(nameof(primerApellido));
        SegundoApellido = segundoApellido;
    }

    public void ActualizarUbicacion(Ubicacion ubicacion)
    {
        Ubicacion = ubicacion.ValidateNull(nameof(ubicacion));
    }

    public void ActualizarContactos(string? telefonoFijo, string? telefonoMovil, string? telefonoFax, string? sitioWeb, string? email)
    {
        TelefonoFijo = telefonoFijo;
        TelefonoMovil = telefonoMovil;
        TelefonoFax = telefonoFax;
        SitioWeb = sitioWeb;
        Email = email.ValidateNotNullOrEmpty(nameof(email));
    }

    public void ActualizarRazonSocial(string? razonSocial)
    {
        RazonSocial = razonSocial;
    }

    public class Builder
    {
        private TipoPersona _tipoPersona;
        private Identificacion _identificacion;
        private string _primerNombre;
        private string _primerApellido;
        private Ubicacion _ubicacion;
        private string _email;
        private string? _segundoNombre;
        private string? _segundoApellido;
        private string? _razonSocial;
        private string? _telefonoFijo;
        private string? _telefonoMovil;
        private string? _telefonoFax;
        private string? _sitioWeb;

        public Builder() { }

        public Builder ConTipoPersona(TipoPersona tipoPersona)
        {
            _tipoPersona = tipoPersona.ValidateNull(nameof(tipoPersona));
            return this;
        }
        public Builder ConIdentificacion(Identificacion identificacion)
        {
            _identificacion = identificacion.ValidateNull(nameof(identificacion));
            return this;
        }

        public Builder ConNombres(string primerNombre, string? segundoNombre, string primerApellido, string? segundoApellido)
        {
            _primerNombre = primerNombre.ValidateNotNullOrEmpty(nameof(primerNombre));
            _segundoNombre = segundoNombre;
            _primerApellido = primerApellido.ValidateNotNullOrEmpty(nameof(primerApellido));
            _segundoApellido = segundoApellido;
            return this;
        }

        public Builder ConUbicacion(Ubicacion ubicacion)
        {
            _ubicacion = ubicacion.ValidateNull(nameof(ubicacion));
            return this;
        }

        public Builder ConContactos(string? telefonoFijo, string? telefonoMovil, string? telefonoFax, string? sitioWeb, string? email)
        {
            _telefonoFijo = telefonoFijo;
            _telefonoMovil = telefonoMovil;
            _telefonoFax = telefonoFax;
            _sitioWeb = sitioWeb;
            _email = email.ValidateNotNullOrEmpty(nameof(email));
            return this;
        }

        public Builder ConRazonSocial(string? razonSocial)
        {
            _razonSocial = razonSocial;
            return this;
        }

        public Tercero Build()
        {
            return new Tercero
            {
                TipoPersona = _tipoPersona.ValidateNull("TipoPersona"),
                Identificacion = _identificacion.ValidateNull("Identificacion"),
                PrimerNombre = _primerNombre.ValidateNotNullOrEmpty("primerNombre"),
                SegundoNombre = _segundoNombre,
                PrimerApellido = _primerApellido.ValidateNotNullOrEmpty("primerApellido"),
                SegundoApellido = _segundoApellido,
                RazonSocial = _razonSocial,
                Ubicacion = _ubicacion,
                TelefonoFijo = _telefonoFijo,
                TelefonoMovil = _telefonoMovil,
                TelefonoFax = _telefonoFax,
                SitioWeb = _sitioWeb,
                Email = _email
            };
        }
    }
}
