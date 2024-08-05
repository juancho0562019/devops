
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.TiposDocumentos.Queries.Get;
public class GetDocumentoConstitucionRequest : IRequest<Result<DocumentoConstitucionDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;


}

public class GetDocumentoConstitucionHandler : IRequestHandler<GetDocumentoConstitucionRequest, Result<DocumentoConstitucionDto?>>
{

    private readonly IReadOnlyRepository<DocumentoConstitucion, string> _documentoConstitucionRepository;
    private readonly IMapper _mapper;

    public GetDocumentoConstitucionHandler(IReadOnlyRepository<DocumentoConstitucion, string> documentoConstitucionRepository, IMapper mapper)
    {
        _documentoConstitucionRepository = documentoConstitucionRepository;
        _mapper = mapper;
    }

    public async Task<Result<DocumentoConstitucionDto?>> Handle(GetDocumentoConstitucionRequest request, CancellationToken cancellationToken)
    {
        var documentoConstitucion = await _documentoConstitucionRepository.GetByIdAsync(request.Id);

        if (documentoConstitucion == null)
        {
            return Result<DocumentoConstitucionDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<DocumentoConstitucionDto?>.Success(_mapper.Map<DocumentoConstitucionDto>(documentoConstitucion));
    }
}
