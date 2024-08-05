using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities
{
    public class ActaConstitucion : BaseEntity<int>
    {
        public string? CaracterTerritorialId { get; private set; }
        public CaracterTerritorial CaracterTerritorial { get; set; } = null!;
        public int? NivelAtencionId { get; private set; }
        public NivelAtencion NivelAtencion { get; set; } = null!;
        public string? EmpresaSocialEstado { get; private set; }
        public string? ActoConstitucionId { get; private set; }
        public DocumentoConstitucion ActoConstitucion { get; set; } = null!;
        public string? NumeroActo { get; private set; }
        public DateTime FechaActo { get; private set; }
        public string EntidadExpide { get; private set; } = string.Empty;
        public string CiudadExpedicion { get; private set; } = string.Empty;

        private ActaConstitucion() { }

 
        public class Builder
        {
            private string? _caracterTerritorial;
            private int? _nivelAtencion;
            private string? _empresaSocialEstado;
            private string? _actoConstitucion;
            private string? _numeroActo;
            private DateTime _fechaActo;
            private string _entidadExpide = string.Empty;
            private string _ciudadExpedicion = string.Empty;
            public Builder()
            {
                
            }

            public Builder ConCaracterTerritorial(string? caracterTerritorial)
            {
                _caracterTerritorial = caracterTerritorial;
                return this;
            }

            public Builder ConNivelAtencion(int? nivelAtencion)
            {
                _nivelAtencion = nivelAtencion;
                return this;
            }

            public Builder ConEmpresaSocialEstado(string? empresaSocialEstado)
            {
                _empresaSocialEstado = empresaSocialEstado;
                return this;
            }

            public Builder ConActoConstitucion(string? actoConstitucion)
            {
                _actoConstitucion = actoConstitucion;
                return this;
            }

            public Builder ConNumeroActo(string? numeroActo)
            {
                _numeroActo = numeroActo;
                return this;
            }

            public Builder ConFechaActo(DateTime? fechaActo)
            {
                
                _fechaActo = fechaActo.ValidateNull(nameof(fechaActo))??DateTime.MinValue;
                return this;
            }

            public Builder ConEntidadExpide(string? entidadExpide)
            {
                _entidadExpide = entidadExpide.ValidateNotNullOrEmpty(parameterName: nameof(entidadExpide));
                return this;
            }

            public Builder ConCiudadExpedicion(string? ciudadExpedicion)
            {
                _ciudadExpedicion = ciudadExpedicion.ValidateNotNullOrEmpty(parameterName: nameof(ciudadExpedicion));
                return this;
            }

            public ActaConstitucion Build()
            {
                
                return new ActaConstitucion() 
                {
                    CaracterTerritorialId = _caracterTerritorial,
                    NivelAtencionId = _nivelAtencion,
                    EmpresaSocialEstado = _empresaSocialEstado,
                    ActoConstitucionId = _actoConstitucion,
                    NumeroActo = _numeroActo,
                    FechaActo = _fechaActo,
                    EntidadExpide = _entidadExpide,
                    CiudadExpedicion = _ciudadExpedicion
                };
            }
        }
    }
}
