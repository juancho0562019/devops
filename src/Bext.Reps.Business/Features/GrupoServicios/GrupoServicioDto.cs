using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.Modalidades;
using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Business.Features.GrupoServicios;
public class GrupoServicioDto 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ModalidadDto Modalidad { get; set; } = null!;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<GrupoServicio, GrupoServicioDto>();
        }
    }

}
