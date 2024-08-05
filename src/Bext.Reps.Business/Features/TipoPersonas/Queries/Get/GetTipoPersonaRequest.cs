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

namespace Bext.Reps.Business.Features.TipoPersonas.Queries.Get;
public class GetTipoPersonaRequest : IRequest<Result<TipoPersonaDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;


}

public class GetTipoPersonaHandler : IRequestHandler<GetTipoPersonaRequest, Result<TipoPersonaDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<TipoPersona, string> _tipoPersonaRepository;
    public GetTipoPersonaHandler(IReadOnlyRepository<TipoPersona, string> tipoPersonaRepository, IMapper mapper)
    {
        _tipoPersonaRepository = tipoPersonaRepository;
        _mapper = mapper;
    }

    public async Task<Result<TipoPersonaDto?>> Handle(GetTipoPersonaRequest request, CancellationToken cancellationToken)
    {
        var tipoPersona = await _tipoPersonaRepository.GetByIdAsync(request.Id);

        if (tipoPersona == null)
        {
            return Result<TipoPersonaDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<TipoPersonaDto?>.Success(_mapper.Map<TipoPersonaDto>(tipoPersona));

    }
}
