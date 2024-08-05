using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Commons.ValueObjects;

namespace Bext.Reps.Domain.Entities;

public sealed class Sede : BaseEntity<int>
{
    public bool EsPrincipal { get; set; }
    public required string Nombre { get; init; }
    public required string NombreResponsable { get; init; }
    public required Ubicacion Ubicacion { get; init; }
    public Contacto DatosContacto { get; set; } = null!;
    public string? CentroPoblado { get; set; }
    public string? Zona { get; set; }
    public string Barrio { get; set; } = string.Empty;
    public int EntidadId { get; set; }
    public Entidad? Entidad { get; set; }
    public ICollection<DocumentoSede> Documentos { get; set; } = [];
    public void AddDocumentoSede(DocumentoSede documento)
    {
        documento.ValidateNull(parameterName: nameof(documento));
        Documentos.Add(documento);
    }
    private Sede() { }

    public class Builder
    {
        private bool _esPrincipal;
        private string _nombre = string.Empty;
        private string _nombreResponsable = string.Empty;
        private Ubicacion _ubicacion = null!;
        private Contacto _contacto = null!;
        private string _zona = string.Empty;
        private string? _centroPoblado;
        private string _barrio = string.Empty;
        private int _entidadId;

        public Builder ConEsPrincipal(bool esPrincipal)
        {
            _esPrincipal = esPrincipal;
            return this;
        }

        public Builder ConNombre(string nombre)
        {
            _nombre = nombre.ValidateNotNullOrEmpty(nameof(nombre));
            return this;
        }

        public Builder ConNombreResponsable(string nombreResponsable)
        {
            _nombreResponsable = nombreResponsable.ValidateNotNullOrEmpty(nameof(nombreResponsable));
            return this;
        }

        public Builder ConUbicacion(Ubicacion ubicacion)
        {
            _ubicacion = ubicacion.ValidateNull(nameof(ubicacion));
            return this;
        }

        public Builder ConContacto(Contacto contacto)
        {
            _contacto = contacto.ValidateNull(nameof(contacto));
            return this;
        }

        public Builder ConZona(string zona)
        {
            _zona = zona ?? string.Empty;
            return this;
        }

        public Builder ConCentroPoblado(string? centroPoblado)
        {
            _centroPoblado = centroPoblado ?? string.Empty;
            return this;
        }

        public Builder ConBarrio(string barrio)
        {
            _barrio = barrio ?? string.Empty;
            return this;
        }

        public Builder ConEntidadId(int entidadId)
        {
            _entidadId = entidadId;
            return this;
        }

        public Sede Build()
        {
            return new Sede
            {
                EsPrincipal = _esPrincipal.ValidateNull(message: DefaultMessage.IsRequired, parameterName: nameof(EsPrincipal)),
                Nombre = _nombre.ValidateNull(parameterName: nameof(Nombre),message: DefaultMessage.IsRequired),
                NombreResponsable = _nombreResponsable.ValidateNull(parameterName: nameof(NombreResponsable), DefaultMessage.IsRequired),
                Ubicacion = _ubicacion.ValidateNull(parameterName: nameof(Ubicacion),DefaultMessage.IsRequired),
                DatosContacto = _contacto.ValidateNull(parameterName: nameof(DatosContacto), DefaultMessage.IsRequired),
                Zona = _zona,
                Barrio = _barrio.ValidateNull(parameterName: nameof(Barrio),DefaultMessage.IsRequired),
                EntidadId = _entidadId,
                CentroPoblado = _centroPoblado
            };
        }
    }
}
