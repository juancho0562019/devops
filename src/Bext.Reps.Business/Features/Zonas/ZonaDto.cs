
using System.Diagnostics.CodeAnalysis;

using AutoMapper;
using Bext.Reps.Domain.Entities.DirectorioGeneral;

namespace Bext.Reps.Business.Features.Zonas;
public class ZonaDto
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {

        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<ItemTablaReferencia, ZonaDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
        }
    }

}
