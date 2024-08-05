using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Commons.ValueObjects;

namespace Bext.Reps.Domain.Entities;
public sealed class PeriodoRepresentacion : BaseEntity<int>
{
    public TipoRepresentacion TipoRepresentacion { get; private set; }
    public int ContactoId { get; private set; }
    public ContactoEntidad Contacto { get; set; } = null!;
    public int EntidadId { get; private set; }
    public Entidad Entidad { get; set; } = null!;
    public DateTime FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }

    public static PeriodoRepresentacion Crear(TipoRepresentacion tipoRepresentacion, DateTime fechaInicio) 
    {
        return new PeriodoRepresentacion() 
        {
            TipoRepresentacion = tipoRepresentacion.ValidateNull(nameof(TipoRepresentacion)),
            FechaInicio = fechaInicio,
        };
    }
    public class Builder
    {
        private Nombre _nombre = null!;
        private Identificacion _identificacion = null!;
        private TipoRepresentacion _tipoRepresentacion;
        private DateTime _fechaInicio;
        public Builder ConNombres(string primerNombre, string? segundoNombre, string primerApellido, string? segundoApellido)
        {
            primerNombre.ValidateNotNullOrEmpty();
            primerApellido.ValidateNotNullOrEmpty();
            _nombre = Nombre.Crear(null, primerNombre, primerApellido, segundoNombre, segundoApellido);
            return this;
        }

        public Builder ConIdentificacion(string tipoIdentificacion, string numeroDocumento)
        {
            tipoIdentificacion.ValidateNotNullOrEmpty();
            numeroDocumento.ValidateNotNullOrEmpty();
            _identificacion = Identificacion.Crear(tipoIdentificacion, numeroDocumento, true);
            return this;
        }

        public Builder ConPeriodoRepresentacion(TipoRepresentacion tipoRepresentacion, DateTime fechaInicioRepresentacion)
        {
            tipoRepresentacion.ValidateNull(nameof(tipoRepresentacion));
            _tipoRepresentacion = tipoRepresentacion;
            _fechaInicio = fechaInicioRepresentacion;
            return this;
        }

        public PeriodoRepresentacion Build()
        {
            _nombre.ValidateNull(nameof(_nombre));
            _identificacion.ValidateNull(nameof(_identificacion));
            
            return new PeriodoRepresentacion
            {
                Contacto = ContactoEntidad.Crear(_nombre, _identificacion),
                TipoRepresentacion = _tipoRepresentacion,
                FechaInicio = _fechaInicio
            };
            
        }
    }

}
