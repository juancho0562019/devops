
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Features.CentrosPoblados;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Entities.DirectorioGeneral;
using MediatR;

namespace Bext.Reps.Business.Features.Zonas.Queries.Get;
public class GetZonaQuery : IRequest<Result<ZonaDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}
public class GetBarrioQueryHandler : IRequestHandler<GetZonaQuery, Result<ZonaDto?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetBarrioQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ZonaDto?>> Handle(GetZonaQuery request, CancellationToken cancellationToken)
    {
        var zona = await _repository.GetDepartamentosByIdAsync(request.Id);

        if (zona is null)
            return Result<ZonaDto?>.Failure($"No se encontraron datos con el codigo {request.Id}");

        var zonaDto = _mapper.Map<ZonaDto>(zona);
        return Result<ZonaDto?>.Success(zonaDto);
    }    
}
