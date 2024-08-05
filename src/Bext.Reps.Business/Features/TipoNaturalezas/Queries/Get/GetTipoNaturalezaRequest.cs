using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.TipoNaturalezas.Queries.Get;
public class GetTipoNaturalezaRequest : IRequest<Result<TipoNaturalezaDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;


}

public class GetTipoNaturalezaHandler : IRequestHandler<GetTipoNaturalezaRequest, Result<TipoNaturalezaDto?>>
{

    private readonly IReadOnlyRepository<TipoNaturaleza, string> _tipoNaturalezaRepository;
    private readonly IMapper _mapper;
    public GetTipoNaturalezaHandler(IReadOnlyRepository<TipoNaturaleza, string> tipoNaturalezaRepository, IMapper mapper)
    {
        _tipoNaturalezaRepository = tipoNaturalezaRepository;
        _mapper = mapper;
    }

    public async Task<Result<TipoNaturalezaDto?>> Handle(GetTipoNaturalezaRequest request, CancellationToken cancellationToken)
    {
        var tipoNaturaleza = await _tipoNaturalezaRepository.GetByIdAsync(request.Id);

        if (tipoNaturaleza == null)
        {
            return Result<TipoNaturalezaDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<TipoNaturalezaDto?>.Success(_mapper.Map<TipoNaturalezaDto>(tipoNaturaleza));

    }
}
