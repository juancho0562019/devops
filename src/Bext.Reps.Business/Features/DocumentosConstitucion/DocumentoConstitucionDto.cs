using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Business.Features.TiposDocumentos;
public class DocumentoConstitucionDto
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;    

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {

        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<DocumentoConstitucion, DocumentoConstitucionDto>();
        }
    }
}
