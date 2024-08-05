using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Bext.Reps.Business.Features.Estandares;

namespace Bext.Reps.Business.Features.Servicios.Queries.GetAllEstandar;
public class EstandarServicioDto : ServicioDto
{
    public ICollection<EstandarPorServicioDto> Estandares { get; set; } = [];

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Servicio, EstandarServicioDto>()
                .ForMember(dest => dest.Estandares, opt => opt.MapFrom(src => src.Estandares));
        }
    }
}

public class EstandarPorServicioDto
{
    public EstandarDto Estandar { get; set; } = null!;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<EstandarPorServicio, EstandarPorServicioDto>()
                .ForMember(dest => dest.Estandar, opt => opt.MapFrom(src => src.Estandar));
        }
    }
}
