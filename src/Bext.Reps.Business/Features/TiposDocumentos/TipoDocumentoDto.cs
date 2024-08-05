using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Business.Features.TiposDocumentos;
public class TipoDocumentoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public TipoDocumentoPrestador Tipo { get; set; }

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {

        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<TipoDocumento, TipoDocumentoDto>();
        }
    }
}
