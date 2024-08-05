
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.TiposDocumentos.Queries.GetAll;
public class GetDocumentoConstitucionAllRequest : IRequest<Result<IEnumerable<DocumentoConstitucionDto>?>>
{
    public string? Id { get; set; }

    public string? Nombre { get; set; }


}

public class GetDocumentoConstitucionAllHandler : IRequestHandler<GetDocumentoConstitucionAllRequest, Result<IEnumerable<DocumentoConstitucionDto>?>>
{

    private readonly IReadOnlyRepository<DocumentoConstitucion, string> _documentoConstitucionRepository;
    private readonly IMapper _mapper;

    public GetDocumentoConstitucionAllHandler(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository, IMapper mapper)
    {
        _documentoConstitucionRepository = documentoConstitucionRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<DocumentoConstitucionDto>?>> Handle(GetDocumentoConstitucionAllRequest request, CancellationToken cancellationToken)
    {
        Func<DocumentoConstitucion, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id??"", request.Nombre??"" };
        IEnumerable<DocumentoConstitucion>? documentosConstitucion = await _documentoConstitucionRepository.GetAllAsync(filter, args);

        if (documentosConstitucion is null || !documentosConstitucion.Any())
            return Result<IEnumerable<DocumentoConstitucionDto>?>.Failure("No hay datos para el filtro proporcionado");

        var documentoConstitucionDto = documentosConstitucion.Select(documentosConstitucion => _mapper.Map<DocumentoConstitucionDto>(documentosConstitucion)).ToList();

        return Result<IEnumerable<DocumentoConstitucionDto>?>.Success(documentoConstitucionDto);
    }
    private Func<DocumentoConstitucion, bool> BuildFilter(string? id, string? nombre)
    {
        return x =>
        {
            bool matches = true;
            if (!string.IsNullOrEmpty(id))
            {
                matches &= x.Id.Equals(id);
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                matches &= x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase);
            }
            return matches;
        };
    }
}
