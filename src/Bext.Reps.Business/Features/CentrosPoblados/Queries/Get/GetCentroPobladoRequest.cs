
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Features.CentrosPoblados;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Entities.DirectorioGeneral;
using MediatR;

namespace Bext.Reps.Business.Features.CentrosPoblados.Queries.Get;
public class GetCentroPobladoQuery : IRequest<Result<CentroPobladoDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}
public class GetCentroPobladoQueryHandler : IRequestHandler<GetCentroPobladoQuery, Result<CentroPobladoDto?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetCentroPobladoQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CentroPobladoDto?>> Handle(GetCentroPobladoQuery request, CancellationToken cancellationToken)
    {
        var centro = await _repository.GetDepartamentosByIdAsync(request.Id);

        if (centro is null)
            return Result<CentroPobladoDto?>.Failure($"No se encontraron datos con el codigo {request.Id}");

        var centroDto = _mapper.Map<CentroPobladoDto>(centro);
        return Result<CentroPobladoDto?>.Success(centroDto);
    }
}
