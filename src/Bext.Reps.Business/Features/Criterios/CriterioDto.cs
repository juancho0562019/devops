using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.Estandares;
using Newtonsoft.Json;

namespace Bext.Reps.Business.Features.Criterios;
public class CriterioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public EstandarDto Estandar { get; set; } = null!;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Criterio, CriterioDto>();
        }
    }

}
