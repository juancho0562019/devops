using Bext.Reps.Domain.Commons.Extensions;
using Newtonsoft.Json;

namespace Bext.Reps.Business.Commons.Models;
public class Anexo
{
    [JsonProperty("NombreAnexo")]
    public string NombreAnexo { get; set; } = string.Empty;

    [JsonProperty("NombreArchivo")]
    public string NombreArchivo { get; set; } = string.Empty;

    [JsonProperty("ExtensionArchivo")]
    public string ExtensionArchivo { get; set; } = string.Empty;

    [JsonProperty("ArchivoBase64")]
    public string ArchivoBase64 { get; set; } = string.Empty;
}

public class Imagen
{
    [JsonProperty("NombreImagen")]
    public string NombreImagen { get; set; } = string.Empty;

    [JsonProperty("ExtensionArchivo")]
    public string ExtensionArchivo { get; set; } = string.Empty;

    [JsonProperty("ArchivoBase64")]
    public string ArchivoBase64 { get; set; } = string.Empty;
}

public class RadicadoRequest
{
    [JsonProperty("Asunto")]
    public string Asunto { get; private set; } = string.Empty;

    [JsonProperty("Prioridad")]
    public string Prioridad { get; private set; } = string.Empty;

    [JsonProperty("Folios")]
    public string Folios { get; private set; } = string.Empty;

    [JsonProperty("IdTRDC")]
    public decimal IdTRDC { get; private set; }

    [JsonProperty("Tercero")]
    public TerceroControlDoc Tercero { get; private set; } = null!;

    [JsonProperty("Imagen")]
    public Imagen? Imagen { get; private set; } = null!;

    [JsonProperty("Anexos")]
    public List<Anexo> Anexos { get; private set; } = new List<Anexo>();
    public void AddAnexo(Anexo anexo)
    {
        Anexos.ValidateNull(parameterName: nameof(anexo));
        Anexos.Add(anexo);
    }
    private RadicadoRequest() { }
    public class Builder
    {
        private string _asunto = string.Empty;
        private string _prioridad= string.Empty;
        private string _folios = string.Empty;
        private decimal _idTRDC;
        private TerceroControlDoc _tercero = null!;
        private Imagen? _imagen;

        public Builder()
        {
           
        }

        public Builder ConAsunto(string asunto)
        {
            _asunto = asunto;
            return this;
        }

        public Builder ConPrioridad(string prioridad)
        {
            _prioridad = prioridad;
            return this;
        }

        public Builder ConFolios(string folios)
        {
            _folios = folios;
            return this;
        }

        public Builder ConIdTRDC(decimal idTRDC)
        {
            _idTRDC = idTRDC;
            return this;
        }

        public Builder ConTercero(TerceroControlDoc tercero)
        {
            _tercero = tercero;
            return this;
        }

        public Builder ConImagen(Imagen imagen)
        {
            _imagen = imagen;
            return this;
        }

        
        public RadicadoRequest Build()
        {
            return new RadicadoRequest 
            {
                Asunto = _asunto.ValidateNotNullOrEmpty(nameof(Asunto)),
                Prioridad = _prioridad.ValidateNotNullOrEmpty(nameof(Prioridad)),
                Folios = _folios.ValidateNotNullOrEmpty(nameof(Folios)),
                IdTRDC = _idTRDC,
                Tercero = _tercero.ValidateNull(nameof(Tercero)),
                Imagen = _imagen,
                
            };
        }
    }
}

public class TerceroControlDoc
{
    [JsonProperty("TipoPersona")]
    public string TipoPersona { get; set; } = string.Empty;

    [JsonProperty("TipoIdentificacion")]
    public string TipoIdentificacion { get; set; } = string.Empty;

    [JsonProperty("NumeroIdentificacion")]
    public string NumeroIdentificacion { get; set; } = string.Empty;

    [JsonProperty("Nombres")]
    public string Nombres { get; set; } = string.Empty;

    [JsonProperty("Apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    [JsonProperty("Pais")]
    public string Pais { get; set; } = "Colombia";

    [JsonProperty("Departamento")]
    public string Departamento { get; set; } = string.Empty;

    [JsonProperty("Municipio")]
    public string Municipio { get; set; } = string.Empty;

    [JsonProperty("Direccion")]
    public string Direccion { get; set; } = string.Empty;

    [JsonProperty("Correo")]
    public string Correo { get; set; } = string.Empty;

    [JsonProperty("Telefono")]
    public string Telefono { get; set; } = string.Empty;

    [JsonProperty("Celular")]
    public string Celular { get; set; } = string.Empty;

    [JsonProperty("Fax")]
    public string Fax { get; set; } = string.Empty;

    [JsonProperty("Pagina")]
    public string Pagina { get; set; } = string.Empty;

    [JsonProperty("Naturaleza")]
    public string Naturaleza { get; set; } = string.Empty;

    public class TerceroBuilder
    {
        private string _tipoPersona = string.Empty;
        private string _tipoIdentificacion = string.Empty;
        private string _numeroIdentificacion = string.Empty;
        private string _nombres = string.Empty;
        private string _apellidos = string.Empty;
        private string _pais = "Colombia";
        private string _departamento = string.Empty;
        private string _municipio = string.Empty;
        private string _direccion = string.Empty;
        private string _correo = string.Empty;
        private string _telefono = string.Empty;
        private string _celular = string.Empty;
        private string _fax = string.Empty;
        private string _pagina = string.Empty;
        private string? _naturaleza;

        public TerceroBuilder() { }

        public TerceroBuilder ConTipoPersona(string? tipoPersona)
        {
            _tipoPersona = tipoPersona.ValidateNotNullOrEmpty();
            return this;
        }

        public TerceroBuilder ConTipoIdentificacion(string? tipoIdentificacion)
        {
            _tipoIdentificacion = tipoIdentificacion.ValidateNotNullOrEmpty();
            return this;
        }

        public TerceroBuilder ConNumeroIdentificacion(string? numeroIdentificacion)
        {
            _numeroIdentificacion = numeroIdentificacion.ValidateNotNullOrEmpty(nameof(numeroIdentificacion));
            return this;
        }

        public TerceroBuilder ConNombres(string? nombres)
        {
            _nombres = nombres.ValidateNotNullOrEmpty(nameof(nombres));
            return this;
        }

        public TerceroBuilder ConApellidos(string? apellidos)
        {
            _apellidos = apellidos.ValidateNotNullOrEmpty(nameof(apellidos));
            return this;
        }

        public TerceroBuilder ConPais(string pais)
        {
            _pais = pais;
            return this;
        }

        public TerceroBuilder ConDepartamento(string? departamento)
        {
            _departamento = departamento.ValidateNotNullOrEmpty(nameof(departamento));
            return this;
        }

        public TerceroBuilder ConMunicipio(string? municipio)
        {
            _municipio = municipio.ValidateNotNullOrEmpty(nameof(municipio));
            return this;
        }

        public TerceroBuilder ConDireccion(string? direccion)
        {
            _direccion = direccion.ValidateNotNullOrEmpty(nameof(direccion));
            return this;
        }

        public TerceroBuilder ConCorreo(string? correo)
        {
            _correo = correo.ValidateNotNullOrEmpty(nameof(correo));
            return this;
        }

        public TerceroBuilder ConTelefono(string? telefono)
        {
            _telefono = telefono.ValidateNotNullOrEmpty();
            return this;
        }

        public TerceroBuilder ConCelular(string celular)
        {
            _celular = celular;
            return this;
        }

        public TerceroBuilder ConFax(string fax)
        {
            _fax = fax;
            return this;
        }

        public TerceroBuilder ConPagina(string pagina)
        {
            _pagina = pagina;
            return this;
        }

        public TerceroBuilder ConNaturaleza(string? naturaleza)
        {
            _naturaleza = naturaleza;
            return this;
        }

        public TerceroControlDoc Build()
        {
            return new TerceroControlDoc
            {
                TipoPersona = _tipoPersona,
                TipoIdentificacion = _tipoIdentificacion,
                NumeroIdentificacion = _numeroIdentificacion,
                Nombres = _nombres,
                Apellidos = _apellidos,
                Pais = _pais,
                Departamento = _departamento,
                Municipio = _municipio,
                Direccion = _direccion,
                Correo = _correo,
                Telefono = _telefono,
                Celular = _celular,
                Fax = _fax,
                Pagina = _pagina,
                Naturaleza = _naturaleza
            };
        }
    }
}
