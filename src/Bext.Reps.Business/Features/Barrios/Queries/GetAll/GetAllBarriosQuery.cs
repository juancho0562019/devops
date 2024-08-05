using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Features.Barrios;
using Bext.Reps.Business.Features.CentrosPoblados;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Barrios.Queries.GetAll;
public class GetAllBarrioQuery : IRequest<Result<IEnumerable<BarrioDto>?>>
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public bool? Estado { get; set; }
}

public class GetAllBarrioQueryHandler : IRequestHandler<GetAllBarrioQuery, Result<IEnumerable<BarrioDto>?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetAllBarrioQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<BarrioDto>?>> Handle(GetAllBarrioQuery request, CancellationToken cancellationToken)
    {
        var barrios = await _repository.GetDepartamentosAsync(d =>
            (string.IsNullOrEmpty(request.Id) || d.Codigo == request.Id) &&
            (string.IsNullOrEmpty(request.Nombre) || d.Nombre.Contains(request.Nombre, StringComparison.OrdinalIgnoreCase)) &&
            (!request.Estado.HasValue || d.Habilitado == request.Estado),
            request?.Id??"", request?.Nombre??"", request?.Estado);

        if (barrios is null || !barrios.Any())
            return Result<IEnumerable<BarrioDto>?>.Failure("No se encontraron datos con los parametros enviados");

        var centrosDtos = _mapper.Map<IEnumerable<BarrioDto>>(barrios);
        return Result<IEnumerable<BarrioDto>?>.Success(centrosDtos);
    }
}
