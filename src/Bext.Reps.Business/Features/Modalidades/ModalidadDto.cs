using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace Bext.Reps.Business.Features.Modalidades;
public class ModalidadDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<Modalidad, ModalidadDto>();
        }
    }

}
