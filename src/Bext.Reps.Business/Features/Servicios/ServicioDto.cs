using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.GrupoServicios;
using Newtonsoft.Json;

namespace Bext.Reps.Business.Features.Servicios;
public class ServicioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public GrupoServicioDto GrupoServicio { get; set; } = null!;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Servicio, ServicioDto>();
        }
    }

}
