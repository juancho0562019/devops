
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.Enums;
using MediatR;

namespace Bext.Reps.Business.Features.TipoNaturalezas.Queries.GetAll;
public class GetTipoVinculacionAllRequest : IRequest<Result<IEnumerable<TipoVinculacionDto>?>>
{
    public string? Id { get; set; } = string.Empty;

    public string? Nombre { get; set; } = string.Empty;


}

public class GetTipoVinculacionAllRequestHandler : IRequestHandler<GetTipoVinculacionAllRequest, Result<IEnumerable<TipoVinculacionDto>?>>
{

    private readonly IReadOnlyRepository<TipoVinculacion, string> _tipoVinculacionRepository;
    private readonly IMapper _mapper;
    public GetTipoVinculacionAllRequestHandler(IReadOnlyRepository<TipoVinculacion, string> tipoVinculacionRepository, IMapper mapper)
    {
        _tipoVinculacionRepository = tipoVinculacionRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<TipoVinculacionDto>?>> Handle(GetTipoVinculacionAllRequest request, CancellationToken cancellationToken)
    {
        Func<TipoVinculacion, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id ?? "", request.Nombre ?? "" };
        IEnumerable<TipoVinculacion>? tipoVinculaciones = await _tipoVinculacionRepository.GetAllAsync(filter, args);
        if (tipoVinculaciones is null || !tipoVinculaciones.Any())
            return Result<IEnumerable<TipoVinculacionDto>?>.Failure("No hay datos para el filtro proporcionado");

        var tipoVinculacionesDto = tipoVinculaciones.Select(tipoVinculaciones => _mapper.Map<TipoVinculacionDto>(tipoVinculaciones)).ToList();

        return Result<IEnumerable<TipoVinculacionDto>?>.Success(tipoVinculacionesDto);
    }

    private Func<TipoVinculacion, bool> BuildFilter(string? id, string? nombre)
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
