using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.Criterios;

namespace Bext.Reps.Business.Features.Complejidades;
public class ComplejidadDto 
{
    public int Id { get; set; }
    public string Nivel { get; set; } = string.Empty;    

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Complejidad, ComplejidadDto>();
        }
    }

}
