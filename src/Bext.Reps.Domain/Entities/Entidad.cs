using Bext.Reps.Domain.Primitives;
using Bext.Reps.Domain.ValueObjects;

namespace Bext.Reps.Domain.Entities;

public sealed class Entidad : BaseEntity<int>
{
    public required string TipoPrestador { get; init; }
    public required string TipoNaturalezaJuridica { get; init; }
    public required string TipoEntidad { get; init; }
    public required Identificacion Identificacion { get; init; }
    public required string Nombre { get; init; } 
    public string? Sigla { get; init; }
    public Ubicacion Ubicacion { get; private set; } = null!;
    public required string CorreoElectronico { get; init; }
    public required string TelefonoPrincipal { get; init; }
    public string? TelefonoAdicional { get; init; }
    public required string Direccion { get; init; }
    public int TerceroId { get; set; }
    public Tercero Tercero { get; set; }
    public ActaConstitucion? ActaConstitucion { get; set; }
    public List<Contacto> Contactos { get; private set; } = [];
    public List<RegistroModalidad> RegistrosModalidad { get; private set; } = [];

    public List<DocumentoEntidad> DocumentosEntidad { get; private set; } = [];

    public List<Sede> Sedes { get; private set; } = [];

    public void AddTercero(Tercero tercero)
    {
        ArgumentNullException.ThrowIfNull(tercero);
        
        Tercero = tercero;
    }
    public void AddContacto(Contacto contacto)
    {
        ArgumentNullException.ThrowIfNull(contacto);


        Contactos.Add(contacto);
    }
    private Entidad() { }

    public class Builder
    {
        private string _tipoNaturalezaJuridica = string.Empty;
        private string _tipoEntidad = string.Empty;
        private string _tipoPrestador = string.Empty;
        private Identificacion _identificacion = null!;
        private string _nombre = string.Empty;
        private string? _sigla;
        private Ubicacion _ubicacion = null!;
        private string _correoElectronico = string.Empty;
        private string _telefonoPrincipal = string.Empty;
        private string? _telefonoAdicional;
        private string _direccionEstablecimiento = string.Empty;

        public Builder ConTipoNaturalezaJuridica(string tipoNaturalezaJuridica)
        {
            _tipoNaturalezaJuridica = tipoNaturalezaJuridica;
            return this;
        }

        public Builder ConTipoEntidad(string tipoEntidad)
        {
            _tipoEntidad = tipoEntidad;
            return this;
        }
        public Builder ConTipoPrestador(string tipoPrestador)
        {
            _tipoPrestador = tipoPrestador;
            return this;
        }

        public Builder ConIdentificacion(Identificacion identificacion)
        {
            ArgumentNullException.ThrowIfNull(identificacion);
            _identificacion = identificacion;
            return this;
        }

        public Builder ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public Builder ConSigla(string? sigla)
        {
            _sigla = sigla;
            return this;
        }

        public Builder ConUbicacion(Ubicacion ubicacion)
        {
            _ubicacion = ubicacion;
            return this;
        }


        public Builder ConCorreoElectronico(string correoElectronico)
        {
            _correoElectronico = correoElectronico;
            return this;
        }

        public Builder ConTelefonoPrincipal(string telefonoPrincipal)
        {
            _telefonoPrincipal = telefonoPrincipal;
            return this;
        }

        public Builder ConTelefonoAdicional(string? telefonoAdicional)
        {
            _telefonoAdicional = telefonoAdicional;
            return this;
        }

        public Builder ConDireccionEstablecimiento(string direccionEstablecimiento)
        {
            _direccionEstablecimiento = direccionEstablecimiento;
            return this;
        }


        public Entidad Build()
        {
            return new Entidad
            {
                TipoNaturalezaJuridica = _tipoNaturalezaJuridica,
                TipoEntidad = _tipoEntidad,
                TipoPrestador = _tipoPrestador,
                Identificacion = _identificacion,
                Direccion = _direccionEstablecimiento,
                Nombre = _nombre,
                Sigla = _sigla,
                Ubicacion = _ubicacion,
                CorreoElectronico = _correoElectronico,
                TelefonoPrincipal = _telefonoPrincipal,
                TelefonoAdicional = _telefonoAdicional,

            };
        }
    }
}

