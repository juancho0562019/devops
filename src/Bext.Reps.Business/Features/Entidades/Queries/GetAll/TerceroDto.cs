using AutoMapper;
using Bext.Reps.Business.Features.TipoPersonas;

namespace Bext.Reps.Business.Features.Entidades.Queries.GetAll;
public class TerceroDto
{
    public TipoPersonaDto? TipoPersona { get;set; }
    public string TipoIdentificacion { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public short DigitoVerificacion { get; set; }
    public string? PrimerNombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoNombre { get; set; }
    public string? SegundoApellido { get; set; }
    public string? RazonSocial { get; set; }
    public string Departamento { get; set; } = null!;
    public string Municipio { get; set; } = null!;
    public string Direccion { get; set; } = string.Empty;
    public string? TelefonoFijo { get; set; }
    public string? TelefonoMovil { get; set; }
    public string? TelefonoFax { get; set; }
    public string? SitioWeb { get; set; }
    public string? Email { get; set; }

    public class Mapping : Profile 
    {
        public Mapping() 
        {
            CreateMap<Tercero, TerceroDto>()
                .ForMember(v => v.TipoIdentificacion, opt => opt.MapFrom( b => b.Identificacion.TipoIdentificacion))
                .ForMember(v => v.NumeroDocumento, opt => opt.MapFrom( b => b.Identificacion.NumeroDocumento))
                .ForMember(v => v.DigitoVerificacion, opt => opt.MapFrom( b => b.Identificacion.DigitoVerificacion))
                .ForMember(v => v.PrimerNombre, opt => opt.MapFrom( b => b.Nombre.PrimerNombre))
                .ForMember(v => v.SegundoNombre, opt => opt.MapFrom( b => b.Nombre.SegundoNombre))
                .ForMember(v => v.PrimerApellido, opt => opt.MapFrom( b => b.Nombre.PrimerApellido))
                .ForMember(v => v.SegundoApellido, opt => opt.MapFrom( b => b.Nombre.SegundoApellido))
                .ForMember(v => v.RazonSocial, opt => opt.MapFrom( b => b.Nombre.RazonSocial))
                .ForMember(v => v.Departamento, opt => opt.MapFrom( b => b.Ubicacion.Departamento))
                .ForMember(v => v.Municipio, opt => opt.MapFrom( b => b.Ubicacion.Municipio))
                .ForMember(v => v.Direccion, opt => opt.MapFrom( b => b.Ubicacion.Direccion))
                .ForMember(v => v.TelefonoFijo, opt => opt.MapFrom( b => b.DatosContacto.TelefonoFijo))
                .ForMember(v => v.TelefonoMovil, opt => opt.MapFrom( b => b.DatosContacto.TelefonoMovil))
                .ForMember(v => v.TelefonoFax, opt => opt.MapFrom( b => b.DatosContacto.TelefonoFax))
                .ForMember(v => v.SitioWeb, opt => opt.MapFrom( b => b.DatosContacto.SitioWeb))
                .ForMember(v => v.Email, opt => opt.MapFrom( b => b.DatosContacto.Email))
                ;
        }
    }
}
