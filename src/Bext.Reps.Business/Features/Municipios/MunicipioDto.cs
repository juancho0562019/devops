using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Domain.Entities.DirectorioGeneral;

namespace Bext.Reps.Business.Features.Municipios;
public class MunicipioDto
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {

        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<ItemTablaReferencia, MunicipioDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Extra_I));
        }
    }

}
