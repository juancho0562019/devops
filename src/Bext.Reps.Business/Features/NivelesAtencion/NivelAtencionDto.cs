
using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace Bext.Reps.Business.Features.NivelesAtencion;
public class NivelAtencionDto
{
    public int Id { get; set; }
    public int Nivel { get; set; }
    public string Nombre { get; set; } = string.Empty;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {

        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<NivelAtencion, NivelAtencionDto>();
        }
    }

}
