using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Business.Features.Entidades.Queries.GetAll;
using Bext.Reps.Business.Features.TipoNaturalezas;
using Bext.Reps.Domain.Commons.Extensions;
using Bext.Reps.Domain.Commons.Primitives;
using Newtonsoft.Json;

namespace Bext.Reps.Business.Features.Entidades.Queries;
public class EntidadDto
{
    public TerceroDto Tercero { get; set; } = null!;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public ContactoEntidadDto? Representante { get; set; }

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile 
    {
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping() 
        {
            CreateMap<Entidad, EntidadDto>()
                .ForMember(dest => dest.Representante, opt => opt.MapFrom(src =>
                    src.Periodos
                       .OrderByDescending(p => p.FechaInicio)
                       .Select(p => p.Contacto)
                       .FirstOrDefault()));
        }
    }
}



public class ContactoEntidadDto
{
    public string? PrimerNombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoNombre { get; set; }
    public string? SegundoApellido { get; set; }
    public string? RazonSocial { get; set; }
    public string TipoIdentificacion { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public short DigitoVerificacion { get; set; }
    
    public TipoVinculacionDto? TipoVinculacion { get; set; } = null!;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<ContactoEntidad, ContactoEntidadDto>()
                 .ForMember(v => v.TipoIdentificacion, opt => opt.MapFrom(b => b.Identificacion.TipoIdentificacion))
                .ForMember(v => v.NumeroDocumento, opt => opt.MapFrom(b => b.Identificacion.NumeroDocumento))
                .ForMember(v => v.DigitoVerificacion, opt => opt.MapFrom(b => b.Identificacion.DigitoVerificacion))
                .ForMember(v => v.PrimerNombre, opt => opt.MapFrom(b => b.Nombre.PrimerNombre))
                .ForMember(v => v.SegundoNombre, opt => opt.MapFrom(b => b.Nombre.SegundoNombre))
                .ForMember(v => v.PrimerApellido, opt => opt.MapFrom(b => b.Nombre.PrimerApellido))
                .ForMember(v => v.SegundoApellido, opt => opt.MapFrom(b => b.Nombre.SegundoApellido))
                .ForMember(v => v.RazonSocial, opt => opt.MapFrom(b => b.Nombre.RazonSocial));
        }
    }
}
