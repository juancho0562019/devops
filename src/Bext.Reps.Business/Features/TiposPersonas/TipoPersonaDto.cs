
using AutoMapper;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Business.Features.TiposPersonas;
public class TipoPersonaDto
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    private class Mapping : Profile 
    {
        public Mapping() 
        {
            CreateMap<TipoPersona, TipoPersonaDto>();
        }
    }
}
