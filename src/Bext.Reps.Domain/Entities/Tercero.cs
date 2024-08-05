using Bext.Reps.Domain.Commons.Abstracts;
using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Commons.ValueObjects;

namespace Bext.Reps.Domain.Entities;
public sealed class Tercero : BaseEntity<int>
{
    public TipoPersona TipoPersona { get; private set; }
    public string TipoPersonaId { get; private set; }
    public Identificacion Identificacion { get; private set; } = null!;
    public Nombre Nombre { get; private set; } = null!;
    public Ubicacion Ubicacion { get; private set; } = null!;
    public Contacto DatosContacto { get; private set; } = null!;
    private Tercero() { }


    public string GetNombre()
    {
        if (TipoPersonaId.Equals("PN", StringComparison.OrdinalIgnoreCase))
        {
            return $"{Nombre.PrimerNombre} {Nombre.SegundoNombre} {Nombre.PrimerApellido} {Nombre.SegundoApellido}".Trim();
        }
        else
        {
            return Nombre.RazonSocial ?? string.Empty;
        }
    }
    public void ActualizarTipoPersona(string tipoPersona)
    {
        TipoPersonaId = tipoPersona.ValidateNull(nameof(tipoPersona));
    }
    public void ActualizarIdentificacion(Identificacion identificacion)
    {
        Identificacion = identificacion.ValidateNull(nameof(identificacion));
    }
  
    public void ActualizarNombres(string? razonSocial,string? primerNombre, string? segundoNombre, string? primerApellido, string? segundoApellido)
    {
        Nombre = Nombre.Crear(razonSocial, primerNombre, primerApellido, segundoNombre, segundoApellido);
    }

    public void ActualizarUbicacion(Ubicacion ubicacion)
    {
        Ubicacion = ubicacion.ValidateNull(nameof(ubicacion));
    }

    public void ActualizarDatosContacto(Contacto contacto)
    {
        DatosContacto = contacto;
    }

    public class Builder
    {
   
        private readonly string _tipoPersona = string.Empty;
        private readonly Identificacion _identificacion = null!;
        private Nombre _nombre = null!;
        private Ubicacion _ubicacion = null!;
        private Contacto _datosContacto = null!;
        public Builder(string tipoPersona, Identificacion identificacion) 
        {
           
            _tipoPersona = tipoPersona;
            _identificacion = identificacion;
        }
       
        public Builder ConNombres(string? razonSocial, string? primerNombre, string? segundoNombre, string? primerApellido, string? segundoApellido)
        {
            if (_tipoPersona == "PN")
            {
                if (string.IsNullOrEmpty(primerNombre) || string.IsNullOrEmpty(primerApellido))
                {
                    throw new ArgumentException("Primer nombre y primer apellido son obligatorios para Persona Natural");
                }
            }
            else if (_tipoPersona == "PJ")
            {
                razonSocial.ValidateNotNullOrEmpty(nameof(razonSocial), "Razón social es obligatoria para Persona Jurídica");
            }
            _nombre = Nombre.Crear(razonSocial, primerNombre, primerApellido, segundoNombre, segundoApellido);
            return this;
        }

        public Builder ConUbicacion(Ubicacion ubicacion)
        {
            _ubicacion = ubicacion.ValidateNull(nameof(ubicacion));
            return this;
        }

        public Builder ConDatosContacto(string? telefonoFijo, string? telefonoMovil, string? telefonoFax, string? sitioWeb, string? email)
        {
            _datosContacto = Contacto.Crear(telefonoFijo, telefonoMovil, telefonoFax, sitioWeb, email);
            return this;
        }


        public Tercero Build()
        {
          
            if (_nombre is null || _ubicacion is null || _datosContacto is null)
            {
                throw new InvalidOperationException("Existen campos obligatorios no enviados");
            }
           
            return new Tercero
            {
                TipoPersonaId = _tipoPersona.ValidateNotNullOrEmpty(parameterName: nameof(TipoPersona)),
                Identificacion = _identificacion.ValidateNull(nameof(Identificacion)),
                Nombre = _nombre,
                Ubicacion = _ubicacion,
                DatosContacto = _datosContacto
            };
        }
    }
}
