using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.TipoNaturalezasPrivadas.Queries.Get;
public class GetSubTipoNaturalezaRequest : IRequest<Result<SubTipoNaturalezaDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}

public class GetSubTipoNaturalezaHandler : IRequestHandler<GetSubTipoNaturalezaRequest, Result<SubTipoNaturalezaDto?>>
{

    private readonly IReadOnlyRepository<SubTipoNaturaleza, string> _tipoNaturalezaPrivadaRepository;
    private readonly IMapper _mapper;
    public GetSubTipoNaturalezaHandler(IReadOnlyRepository<SubTipoNaturaleza, string> tipoNaturalezaPrivadaRepository, IMapper mapper)
    {
        _tipoNaturalezaPrivadaRepository = tipoNaturalezaPrivadaRepository;
        _mapper = mapper;
    }

    public async Task<Result<SubTipoNaturalezaDto?>> Handle(GetSubTipoNaturalezaRequest request, CancellationToken cancellationToken)
    {
        var tipoNaturalezaPrivada = await _tipoNaturalezaPrivadaRepository.GetByIdAsync(request.Id);

        if (tipoNaturalezaPrivada == null)
        {
            return Result<SubTipoNaturalezaDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<SubTipoNaturalezaDto?>.Success(_mapper.Map<SubTipoNaturalezaDto>(tipoNaturalezaPrivada));

    }
}
