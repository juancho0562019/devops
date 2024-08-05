
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.ValueObjects;
using MediatR;

namespace Bext.Reps.Business.Features.TipoPersonas.Queries.GetAll;
public class GetTipoPersonaAllRequest : IRequest<Result<IEnumerable<TipoPersonaDto>?>>
{
    public string Id { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;


}

public class GetTipoPersonaAllRequestHandler : IRequestHandler<GetTipoPersonaAllRequest, Result<IEnumerable<TipoPersonaDto>?>>
{

    private readonly IReadOnlyRepository<TipoPersona, string> _tipoPersonaRepository;
    private readonly IMapper _mapper;
    public GetTipoPersonaAllRequestHandler(IReadOnlyRepository<TipoPersona, string> tipoPersonaRepository, IMapper mapper)
    {
        _tipoPersonaRepository = tipoPersonaRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<TipoPersonaDto>?>> Handle(GetTipoPersonaAllRequest request, CancellationToken cancellationToken)
    {
        Func<TipoPersona, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id, request.Nombre };
        IEnumerable<TipoPersona>? tipoPersonas = await _tipoPersonaRepository.GetAllAsync(filter, args);

        if(tipoPersonas is null || !tipoPersonas.Any())
            return Result<IEnumerable<TipoPersonaDto>?>.Failure("No hay datos para el filtro proporcionado");

        var tipoPersonaDtos = tipoPersonas.Select(tipoPersona => _mapper.Map<TipoPersonaDto>(tipoPersona)).ToList();

        return Result<IEnumerable<TipoPersonaDto>?>.Success(tipoPersonaDtos);
    }


    private Func<TipoPersona, bool> BuildFilter(string? id, string? nombre)
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
