using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.Criterios;

namespace Bext.Reps.Business.Features.Estandares;
public class EstandarDto 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<CriterioDto> Criterios { get; set; } = [];

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Estandar, EstandarDto>();
        }
    }

}
