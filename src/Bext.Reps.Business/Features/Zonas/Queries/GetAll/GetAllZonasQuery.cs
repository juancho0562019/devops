using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Zonas.Queries.GetAll;
public class GetAllZonaQuery : IRequest<Result<IEnumerable<ZonaDto>?>>
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public bool? Estado { get; set; }
}

public class GetAllZonasQueryHandler : IRequestHandler<GetAllZonaQuery, Result<IEnumerable<ZonaDto>?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetAllZonasQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ZonaDto>?>> Handle(GetAllZonaQuery request, CancellationToken cancellationToken)
    {
        var zonas = await _repository.GetDepartamentosAsync(d =>
            (string.IsNullOrEmpty(request.Id) || d.Codigo == request.Id) &&
            (string.IsNullOrEmpty(request.Nombre) || d.Nombre.Contains(request.Nombre, StringComparison.OrdinalIgnoreCase)) &&
            (!request.Estado.HasValue || d.Habilitado == request.Estado),
            request?.Id??"", request?.Nombre??"", request?.Estado);

        if (zonas is null || !zonas.Any())
            return Result<IEnumerable<ZonaDto>?>.Failure("No se encontraron datos con los parametros enviados");

        var centrosDtos = _mapper.Map<IEnumerable<ZonaDto>>(zonas);
        return Result<IEnumerable<ZonaDto>?>.Success(centrosDtos);
    }
}
