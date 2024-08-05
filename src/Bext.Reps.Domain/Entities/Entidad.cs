using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Commons.ValueObjects;

namespace Bext.Reps.Domain.Entities;

public class Entidad : AggregateRoot<int>
{
    public required Identificacion Identificacion { get; init; }
    public required string RazonSocial { get; init; }
    public required Contacto DatosContacto { get; init; }
    public required string Direccion { get; set; } = string.Empty;
    public required string TipoPersonaId { get; init; }
    public TipoPersona TipoPersona { get; init; } = null!;
    public required string TipoPrestadorId { get; init; }
    public ClasePrestador TipoPrestador { get; set; } = null!;
    public string? TipoNaturalezaId { get; init; }
    public TipoNaturaleza? TipoNaturaleza { get; init; }
    public string? SubTipoNaturaleza { get; init; }
    public ActaConstitucion? ActaConstitucion { get; set; }
    public int? ActaConstitucionId { get; set; }
    public Tercero Tercero { get; private set; } = null!;
    public int TerceroId { get; set; }

    public virtual ICollection<DocumentoEntidad> DocumentosEntidad { get; private set; } = [];
    public virtual ICollection<Sede> Sedes { get; private set; } = [];
    public virtual ICollection<PeriodoRepresentacion> Periodos { get; private set; } = [];
    public virtual ICollection<ServicioInscritoSede> Servicios { get; private set; } = [];
    public void AddTercero(Tercero tercero)
    {
        tercero.ValidateNull(parameterName: nameof(tercero));
        Tercero = tercero;
    }

    public void AddActaConstitucion(ActaConstitucion acta)
    {
        ActaConstitucion = acta;
    }

    public void AddDocumentoEntidad(DocumentoEntidad documento)
    {
        documento.ValidateNull(parameterName: nameof(documento));
        DocumentosEntidad.Add(documento);
    }
    public void AddPeriodoRepresentacion(PeriodoRepresentacion periodo)
    {
        periodo.ValidateNull(parameterName: nameof(periodo));
        Periodos.Add(periodo);
    }
    public void AddSede(Sede sede)
    {
        sede.ValidateNull(parameterName: nameof(sede));
        Sedes.Add(sede);
    }

    public void AddSedeDocumento(int sedeId, DocumentoSede documento)
    {
        var sede = Sedes.FirstOrDefault(s => s.Id == sedeId);
        if (sede == null)
        {
            throw new InvalidOperationException("La sede no existe.");
        }

        sede.AddDocumentoSede(documento);
    }

    private Entidad(int id) : base(id) { }

    public class Builder
    {
        private int _id;
        private Identificacion _identificacion = null!;
        private string _razonSocial = string.Empty;
        private Contacto _datosContacto = null!;
        private string? _tipoNaturaleza = string.Empty;
        private string? _subTipoNaturaleza;
        private string _tipoPersona = string.Empty;
        private string _tipoPrestador = string.Empty;
        private string _direccion = string.Empty;
        private ActaConstitucion? _actaConstitucion = null!;
        private Tercero _tercero = null!;
        private List<DocumentoEntidad> _documentosEntidad = new();
        private List<Sede> _sedes = new();

        public Builder(int id)
        {
            _id = id;
        }

        public Builder ConIdentificacion(Identificacion identificacion)
        {
            _identificacion = identificacion.ValidateNull(parameterName: nameof(identificacion));
            return this;
        }

        public Builder ConRazonSocial(string razonSocial)
        {
            _razonSocial = razonSocial.ValidateNotNullOrEmpty(parameterName: nameof(razonSocial));
            return this;
        }

        public Builder ConDireccion(string direccion)
        {
            _direccion = direccion.ValidateNotNullOrEmpty(parameterName: nameof(direccion));
            return this;
        }

        public Builder ConDatosContacto(Contacto datosContacto)
        {
            _datosContacto = datosContacto.ValidateNull(parameterName: nameof(datosContacto));
            return this;
        }

        public Builder ConTipoNaturalezaJuridica(string? tipoNaturaleza, string? subTipoNaturaleza, string? tipoPersona)
        {
            
            
            _tipoNaturaleza = tipoNaturaleza;
            _subTipoNaturaleza = subTipoNaturaleza;
            return this;
        }

        public Builder ConTipoPersona(string tipoPersona)
        {
            _tipoPersona = tipoPersona.ValidateNotNullOrEmpty(parameterName: nameof(tipoPersona));
            return this;
        }

        public Builder ConClasePrestador(string tipoPrestador)
        {
            _tipoPrestador = tipoPrestador.ValidateNotNullOrEmpty(parameterName: nameof(tipoPrestador));
            return this;
        }

        public Builder ConActaConstitucion(ActaConstitucion? acta)
        {
            _actaConstitucion = acta;
            return this;
        }

        public Builder ConTercero(Tercero tercero)
        {
            _tercero = tercero.ValidateNull(parameterName: nameof(tercero));
            return this;
        }

        public Builder ConDocumentosEntidad(List<DocumentoEntidad> documentos)
        {
            _documentosEntidad = documentos.ValidateNull(parameterName: nameof(documentos));
            return this;
        }

        public Builder ConSedes(List<Sede> sedes)
        {
            _sedes = sedes.ValidateNull(parameterName: nameof(sedes));
            return this;
        }

        public Entidad Build()
        {
            ValidateFields();

            var entidad = new Entidad(_id)
            {
                Identificacion = _identificacion,
                RazonSocial = _razonSocial,
                DatosContacto = _datosContacto,
                TipoNaturalezaId = _tipoNaturaleza,
                SubTipoNaturaleza = _subTipoNaturaleza,
                TipoPersonaId = _tipoPersona,
                TipoPrestadorId = _tipoPrestador,
                ActaConstitucion = _actaConstitucion,
                Tercero = _tercero,
                Direccion = _direccion,
            };

            foreach (var documento in _documentosEntidad)
            {
                entidad.AddDocumentoEntidad(documento);
            }

            foreach (var sede in _sedes)
            {
                entidad.AddSede(sede);
            }

            return entidad;
        }

        private void ValidateFields()
        {
            if (_tipoPersona == "PN")
            {
                // Validaciones para Persona Natural
                if (string.IsNullOrEmpty(_razonSocial) || _identificacion is null || _datosContacto is null)
                {
                    throw new InvalidOperationException("Todos los campos son obligatorios para Persona Natural");
                }
            }
            else if (_tipoPersona == "PJ")
            {
                // Validaciones para Persona Jurídica
                _razonSocial.ValidateNotNullOrEmpty("RazonSocial", DefaultMessage.IsRequired);
                _identificacion.ValidateNull("Datos Identificacion", DefaultMessage.IsRequired);
                _datosContacto.ValidateNull("Datos Contacto", DefaultMessage.IsRequired);

                // Naturaleza Jurídica Privada
                if (_tipoNaturaleza == "02")
                {
                    _subTipoNaturaleza.ValidateNotNullOrEmpty("Especificacion de naturaleza juridica privada es obligatoria");
                    
                    
                    if (_subTipoNaturaleza == "21" || _subTipoNaturaleza == "22" || _subTipoNaturaleza == "23")
                    {
                        _actaConstitucion.ValidateNull("ActaConstitucion", DefaultMessage.IsRequired);
                    }
                }

                if (_tipoNaturaleza == "01" || _tipoNaturaleza == "03")
                {
                    _actaConstitucion.ValidateNull("ActaConstitucion", DefaultMessage.IsRequired);
                }
            }
            else
            {
                throw new ArgumentException("Tipo de persona no válido");
            }
        }
    }
}
