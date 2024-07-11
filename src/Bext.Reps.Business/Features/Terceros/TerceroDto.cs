using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.TiposPersonas;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Business.Features.Terceros;
public class TerceroDto
{
    public TipoPersonaDto TipoPersona { get; set; } = null!;
    public string TipoPersonaId { get; set; } = string.Empty;
    public string TipoIdentificacionId { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public short DigitoVerificacion { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public string? RazonSocial { get; set; }
    public string Pais { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string Municipio { get; set; } = string.Empty;
    public string? TelefonoFijo { get; set; }
    public string? TelefonoMovil { get; set; }
    public string? TelefonoFax { get; set; }
    public string? SitioWeb { get; set; }
    public string? Email { get; set; }

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile 
    {
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping() 
        {
            CreateMap<Tercero, TerceroDto>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Ubicacion.Pais))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Ubicacion.Departamento))
                .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Ubicacion.Municipio));
        }
    }
}
