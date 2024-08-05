using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Departamentos.Queries.GetAll;
public class GetAllDepartamentosQuery : IRequest<Result<IEnumerable<DepartamentoDto>?>>
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public bool? Estado { get; set; }
}

public class GetAllDepartamentosQueryHandler : IRequestHandler<GetAllDepartamentosQuery, Result<IEnumerable<DepartamentoDto>?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetAllDepartamentosQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<DepartamentoDto>?>> Handle(GetAllDepartamentosQuery request, CancellationToken cancellationToken)
    {
        var departamentos = await _repository.GetDepartamentosAsync(d =>
            (string.IsNullOrEmpty(request.Id) || d.Codigo == request.Id) &&
            (string.IsNullOrEmpty(request.Nombre) || d.Nombre.Contains(request.Nombre, StringComparison.OrdinalIgnoreCase)) &&
            (!request.Estado.HasValue || d.Habilitado == request.Estado),
            request?.Id??"", request?.Nombre??"", request?.Estado);

        if (departamentos is null || !departamentos.Any())
            return Result<IEnumerable<DepartamentoDto>?>.Failure("No se encontraron datos con los parametros enviados");

        var departamentoDtos = _mapper.Map<IEnumerable<DepartamentoDto>>(departamentos);
        return Result<IEnumerable<DepartamentoDto>?>.Success(departamentoDtos);
    }
}
