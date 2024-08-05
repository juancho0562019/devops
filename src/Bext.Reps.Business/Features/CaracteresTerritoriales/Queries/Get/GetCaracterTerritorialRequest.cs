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
public class GetCaracterTerritorialRequest : IRequest<Result<CaracterTerritorialDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;


}

public class GetCaracterTerritorialRequestHandler : IRequestHandler<GetCaracterTerritorialRequest, Result<CaracterTerritorialDto?>>
{

    private readonly IReadOnlyRepository<CaracterTerritorial, string> _caracterTerritorialRespository;
    private readonly IMapper _mapper;
    public GetCaracterTerritorialRequestHandler(IReadOnlyRepository<CaracterTerritorial, string> caracterTerritorialRespository, IMapper mapper)
    {
        _caracterTerritorialRespository = caracterTerritorialRespository;
        _mapper = mapper;
    }

    public async Task<Result<CaracterTerritorialDto?>> Handle(GetCaracterTerritorialRequest request, CancellationToken cancellationToken)
    {
        var tipoCaracterTerritorial = await _caracterTerritorialRespository.GetByIdAsync(request.Id);

        if (tipoCaracterTerritorial == null)
        {
            return Result<CaracterTerritorialDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<CaracterTerritorialDto?>.Success(_mapper.Map<CaracterTerritorialDto>(tipoCaracterTerritorial));

    }
}
