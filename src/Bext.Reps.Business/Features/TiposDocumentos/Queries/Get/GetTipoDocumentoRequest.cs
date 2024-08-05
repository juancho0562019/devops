
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.TiposDocumentos.Queries.Get;
public class GetTipoDocumentoRequest : IRequest<Result<TipoDocumentoDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }


}

public class GetTipoDocumentoHandler : IRequestHandler<GetTipoDocumentoRequest, Result<TipoDocumentoDto?>>
{

    private readonly IReadOnlyRepository<TipoDocumento, int> _tipoDocumentoRepository;
    private readonly IMapper _mapper;
    public GetTipoDocumentoHandler(IReadOnlyRepository<TipoDocumento, int> tipoDocumentoRepository, IMapper mapper)
    {
        _tipoDocumentoRepository = tipoDocumentoRepository;
        _mapper = mapper;
    }

    public async Task<Result<TipoDocumentoDto?>> Handle(GetTipoDocumentoRequest request, CancellationToken cancellationToken)
    {
        var tipoDocumento = await _tipoDocumentoRepository.GetByIdAsync(request.Id);

        if (tipoDocumento == null)
        {
            return Result<TipoDocumentoDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<TipoDocumentoDto?>.Success(_mapper.Map<TipoDocumentoDto>(tipoDocumento));

    }
}
