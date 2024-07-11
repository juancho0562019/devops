using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.ViewModel;
using AutoMapper;

namespace Bext.Reps.Business;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Contacto, ContactoResponse>().ReverseMap();
        CreateMap<Tercero, TerceroResponse>().ReverseMap();
        CreateMap<Entidad, EntidadResponse>().ReverseMap();
        CreateMap<Entidad, EntidadRequest>().ReverseMap();
        CreateMap<Modalidad, ModalidadResponse>().ReverseMap();
        CreateMap<Modalidad, ModalidadRequest>().ReverseMap();
    }
}
