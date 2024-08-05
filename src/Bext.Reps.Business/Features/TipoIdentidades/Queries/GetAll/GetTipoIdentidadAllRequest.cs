
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.TipoIdentidades.Queries.GetAll;
public class GetTipoIdentidadAllRequest : IRequest<Result<IEnumerable<TipoIdentidadDto>?>>
{
    public string? Id { get; set; } = string.Empty;

    public string? Nombre { get; set; } = string.Empty;


}

public class GetTipoIdentidadAllRequestHandler : IRequestHandler<GetTipoIdentidadAllRequest, Result<IEnumerable<TipoIdentidadDto>?>>
{

    private readonly IReadOnlyRepository<TipoIdentidad, string> _tipoIdentidadRepository;
    private readonly IMapper _mapper;
    public GetTipoIdentidadAllRequestHandler(IReadOnlyRepository<TipoIdentidad, string> tipoIdentidadRepository, IMapper mapper)
    {
        _tipoIdentidadRepository = tipoIdentidadRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<TipoIdentidadDto>?>> Handle(GetTipoIdentidadAllRequest request, CancellationToken cancellationToken)
    {
        Func<TipoIdentidad, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id ?? "", request.Nombre??"" };
        IEnumerable<TipoIdentidad>? tipoIdentidad = await _tipoIdentidadRepository.GetAllAsync(filter, args);

        if (tipoIdentidad is null || !tipoIdentidad.Any())
            return Result<IEnumerable<TipoIdentidadDto>?>.Failure("No hay datos para el filtro proporcionado");

        var tipoIdentidadDtos = tipoIdentidad.Select(tipoIdentidad => _mapper.Map<TipoIdentidadDto>(tipoIdentidad)).ToList();

        return Result<IEnumerable<TipoIdentidadDto>?>.Success(tipoIdentidadDtos);
    }

    private Func<TipoIdentidad, bool> BuildFilter(string? id, string? nombre)
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
