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
public class GetTipoVinculacionRequest : IRequest<Result<TipoVinculacionDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;


}

public class GetTipoVinculacionHandler : IRequestHandler<GetTipoVinculacionRequest, Result<TipoVinculacionDto?>>
{

    private readonly IReadOnlyRepository<TipoVinculacion, string> _tipoVinculacionRepository;
    private readonly IMapper _mapper;
    public GetTipoVinculacionHandler(IReadOnlyRepository<TipoVinculacion, string> tipoVinculacionRepository, IMapper mapper)
    {
        _tipoVinculacionRepository = tipoVinculacionRepository;
        _mapper = mapper;
    }

    public async Task<Result<TipoVinculacionDto?>> Handle(GetTipoVinculacionRequest request, CancellationToken cancellationToken)
    {
        var tipoVinculacion = await _tipoVinculacionRepository.GetByIdAsync(request.Id);

        if (tipoVinculacion == null)
        {
            return Result<TipoVinculacionDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<TipoVinculacionDto?>.Success(_mapper.Map<TipoVinculacionDto>(tipoVinculacion));

    }
}
