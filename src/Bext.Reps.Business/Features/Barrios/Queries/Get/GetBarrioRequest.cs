
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Features.CentrosPoblados;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Entities.DirectorioGeneral;
using MediatR;

namespace Bext.Reps.Business.Features.Barrios.Queries.Get;
public class GetBarrioQuery : IRequest<Result<BarrioDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}
public class GetBarrioQueryHandler : IRequestHandler<GetBarrioQuery, Result<BarrioDto?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetBarrioQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<BarrioDto?>> Handle(GetBarrioQuery request, CancellationToken cancellationToken)
    {
        var barrio = await _repository.GetDepartamentosByIdAsync(request.Id);

        if (barrio is null)
            return Result<BarrioDto?>.Failure($"No se encontraron datos con el codigo {request.Id}");

        var barrioDto = _mapper.Map<BarrioDto>(barrio);
        return Result<BarrioDto?>.Success(barrioDto);
    }    
}
