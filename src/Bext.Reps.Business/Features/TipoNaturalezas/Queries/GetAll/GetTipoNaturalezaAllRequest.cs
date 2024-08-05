
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.TipoNaturalezas.Queries.GetAll;
public class GetTipoNaturalezaAllRequest : IRequest<Result<IEnumerable<TipoNaturalezaDto>?>>
{
    public string? Id { get; set; } = string.Empty;

    public string? Nombre { get; set; } = string.Empty;


}

public class GetTipoNaturalezaAllRequestHandler : IRequestHandler<GetTipoNaturalezaAllRequest, Result<IEnumerable<TipoNaturalezaDto>?>>
{

    private readonly IReadOnlyRepository<TipoNaturaleza, string> _tipoNaturalezaRepository;
    private readonly IMapper _mapper;
    public GetTipoNaturalezaAllRequestHandler(IReadOnlyRepository<TipoNaturaleza, string> tipoNaturalezaRepository, IMapper mapper)
    {
        _tipoNaturalezaRepository = tipoNaturalezaRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<TipoNaturalezaDto>?>> Handle(GetTipoNaturalezaAllRequest request, CancellationToken cancellationToken)
    {
        Func<TipoNaturaleza, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id ?? "", request.Nombre ?? "" };
        IEnumerable<TipoNaturaleza>? tipoNaturaleza = await _tipoNaturalezaRepository.GetAllAsync(filter, args);

        if (tipoNaturaleza is null || !tipoNaturaleza.Any())
            return Result<IEnumerable<TipoNaturalezaDto>?>.Failure("No hay datos para el filtro proporcionado");

        var tipoNaturalezaDtos = tipoNaturaleza.Select(tipoNaturaleza => _mapper.Map<TipoNaturalezaDto>(tipoNaturaleza)).ToList();

        return Result<IEnumerable<TipoNaturalezaDto>?>.Success(tipoNaturalezaDtos);
    }

    private Func<TipoNaturaleza, bool> BuildFilter(string? id, string? nombre)
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
