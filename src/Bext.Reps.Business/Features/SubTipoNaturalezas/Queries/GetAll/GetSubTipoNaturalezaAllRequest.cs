
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.TipoNaturalezasPrivadas;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.SubTipoNaturalezas.Queries.GetAll;
public class GetSubTipoNaturalezaAllRequest : IRequest<Result<IEnumerable<SubTipoNaturalezaDto>?>>
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public string? TipoNaturalezaId { get; set; }
}

public class GetAllSubTipoNaturalezaRequestHandler : IRequestHandler<GetSubTipoNaturalezaAllRequest, Result<IEnumerable<SubTipoNaturalezaDto>?>>
{

    private readonly IReadOnlyRepository<SubTipoNaturaleza, string> _tipoNaturalezaPrivadaRepository;
    private readonly IMapper _mapper;
    public GetAllSubTipoNaturalezaRequestHandler(IReadOnlyRepository<SubTipoNaturaleza, string> tipoNaturalezaPrivadaRepository, IMapper mapper)
    {
        _tipoNaturalezaPrivadaRepository = tipoNaturalezaPrivadaRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<SubTipoNaturalezaDto>?>> Handle(GetSubTipoNaturalezaAllRequest request, CancellationToken cancellationToken)
    {
        Func<SubTipoNaturaleza, bool> filter = BuildFilter(request.Id, request.Nombre, request.TipoNaturalezaId);
        object[] args = { request.Id??"", request.Nombre??"", request.TipoNaturalezaId??"" };
        IEnumerable<SubTipoNaturaleza>? subTipoNaturaleza = await _tipoNaturalezaPrivadaRepository.GetAllAsync(filter, args);

        if (subTipoNaturaleza is null || !subTipoNaturaleza.Any())
            return Result<IEnumerable<SubTipoNaturalezaDto>?>.Failure("No hay datos para el filtro proporcionado");

        var TipoNaturalezaPrivadaDtos = subTipoNaturaleza.Select(tipoNaturalezaPrivada => _mapper.Map<SubTipoNaturalezaDto>(tipoNaturalezaPrivada)).ToList();

        return Result<IEnumerable<SubTipoNaturalezaDto>?>.Success(TipoNaturalezaPrivadaDtos);
    }

    private Func<SubTipoNaturaleza, bool> BuildFilter(string? id, string? nombre, string? tipoNaturaleza)
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
            if (!string.IsNullOrEmpty(tipoNaturaleza))
            {
                matches &= x.TipoNaturalezaId.Equals(tipoNaturaleza, StringComparison.OrdinalIgnoreCase);
            }
            return matches;
        };
    }
}
