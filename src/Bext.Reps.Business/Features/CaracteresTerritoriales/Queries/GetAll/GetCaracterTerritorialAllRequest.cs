using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.TipoNaturalezas.Queries.GetAll;

public class GetCaracterTerritorialAllRequest : IRequest<Result<IEnumerable<CaracterTerritorialDto>?>>
{
    
    public string? Id { get; set; }

    public string? Nombre { get; set; }

}

public class GetCaracterTerritorialAllHandler : IRequestHandler< GetCaracterTerritorialAllRequest, Result<IEnumerable<CaracterTerritorialDto>?>>
{

    private readonly IReadOnlyRepository<CaracterTerritorial, string> _caracterTerritorialRespository;
    private readonly IMapper _mapper;
    public GetCaracterTerritorialAllHandler(IReadOnlyRepository<CaracterTerritorial, string> caracterTerritorialRespository, IMapper mapper)
    {
        _caracterTerritorialRespository = caracterTerritorialRespository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CaracterTerritorialDto>?>> Handle(GetCaracterTerritorialAllRequest request, CancellationToken cancellationToken)
    {
        Func<CaracterTerritorial, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id??"", request.Nombre??"" };
        IEnumerable<CaracterTerritorial>? caracterTerritorial = await _caracterTerritorialRespository.GetAllAsync(filter, args);

        if (caracterTerritorial is null || !caracterTerritorial.Any())
            return Result<IEnumerable<CaracterTerritorialDto>?>.Failure("No hay datos para el filtro proporcionado");

        var caracterTerritorialDto = caracterTerritorial.Select(caracterTerritorial => _mapper.Map<CaracterTerritorialDto>(caracterTerritorial)).ToList();

        return Result<IEnumerable<CaracterTerritorialDto>?>.Success(caracterTerritorialDto);

    }

    private Func<CaracterTerritorial, bool> BuildFilter(string? id, string? nombre)
    {
        return x =>
        {
            bool matches = true;
            if (!string.IsNullOrEmpty(id))
            {
                matches &= x.Id.Equals(id);
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                matches &= x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase);
            }
            return matches;
        };
    }
}
