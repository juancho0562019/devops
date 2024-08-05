using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Features.CentrosPoblados;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.CentrosPoblados.Queries.GetAll;
public class GetAllCentroPobladoQuery : IRequest<Result<IEnumerable<CentroPobladoDto>?>>
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public bool? Estado { get; set; }
}

public class GetAllCentroPobladoQueryHandler : IRequestHandler<GetAllCentroPobladoQuery, Result<IEnumerable<CentroPobladoDto>?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCentroPobladoQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CentroPobladoDto>?>> Handle(GetAllCentroPobladoQuery request, CancellationToken cancellationToken)
    {
        var centros = await _repository.GetDepartamentosAsync(d =>
            (string.IsNullOrEmpty(request.Id) || d.Codigo == request.Id) &&
            (string.IsNullOrEmpty(request.Nombre) || d.Nombre.Contains(request.Nombre, StringComparison.OrdinalIgnoreCase)) &&
            (!request.Estado.HasValue || d.Habilitado == request.Estado),
            request?.Id??"", request?.Nombre??"", request?.Estado);

        if (centros is null || !centros.Any())
            return Result<IEnumerable<CentroPobladoDto>?>.Failure("No se encontraron datos con los parametros enviados");

        var centrosDtos = _mapper.Map<IEnumerable<CentroPobladoDto>>(centros);
        return Result<IEnumerable<CentroPobladoDto>?>.Success(centrosDtos);
    }
}
