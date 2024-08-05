using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.Criterios;

namespace Bext.Reps.Business.Features.Especificidades;
public class EspecificidadDto 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;    

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Especificidad, EspecificidadDto>();
        }
    }

}
