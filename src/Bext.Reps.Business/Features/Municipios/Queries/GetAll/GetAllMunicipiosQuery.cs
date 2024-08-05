
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Municipios.Queries.GetAll;
public class GetAllMunicipiosQuery : IRequest<Result<IEnumerable<MunicipioDto>?>>
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public bool? Estado { get; set; }
    public string? Departamento { get; set; }
}


public class GetAllMunicipiosQueryHandler : IRequestHandler<GetAllMunicipiosQuery, Result<IEnumerable<MunicipioDto>?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;
    public GetAllMunicipiosQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<MunicipioDto>?>> Handle(GetAllMunicipiosQuery request, CancellationToken cancellationToken)
    {

        var vm = await _repository.GetMunicipiosAsync(d =>
            (string.IsNullOrEmpty(request.Departamento) || d.Extra_I == request.Departamento) &&
            (string.IsNullOrEmpty(request.Id) || d.Codigo == request.Id) &&
            (string.IsNullOrEmpty(request.Nombre) || d.Nombre.Contains(request.Nombre, StringComparison.OrdinalIgnoreCase)) &&
            (!request.Estado.HasValue || d.Habilitado == request.Estado),
            request?.Id ?? "", request?.Nombre ?? "", request?.Estado, request?.Departamento ?? "");

        if (vm is null)
            return Result<IEnumerable<MunicipioDto>?>.Failure($"No se encontraron datos con los parametros enviados");
        var municipioDtos = _mapper.Map<IEnumerable<MunicipioDto>>(vm);

        return Result<IEnumerable<MunicipioDto>?>.Success(municipioDtos);
    }
}
