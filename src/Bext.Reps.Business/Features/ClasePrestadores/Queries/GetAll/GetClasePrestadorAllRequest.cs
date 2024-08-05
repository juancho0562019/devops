
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.ClasePrestadores.Queries.GetAll;
public class GetClasePrestadorAllRequest : IRequest<Result<IEnumerable<ClasePrestadorDto>?>>
{
    public string? Id { get; set; } = string.Empty;
    public string? Nombre { get; set; } = string.Empty;
}

public class GetClasePrestadorAllRequestHandler : IRequestHandler<GetClasePrestadorAllRequest, Result<IEnumerable<ClasePrestadorDto>?>>
{

    private readonly IReadOnlyRepository<ClasePrestador, string> _tipoPrestadorRepository;
    private readonly IMapper _mapper;
    public GetClasePrestadorAllRequestHandler(IReadOnlyRepository<ClasePrestador, string> tipoPrestadorRepository, IMapper mapper)
    {
        _tipoPrestadorRepository = tipoPrestadorRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<ClasePrestadorDto>?>> Handle(GetClasePrestadorAllRequest request, CancellationToken cancellationToken)
    {
        Func<ClasePrestador, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id??"", request.Nombre??"" };
        IEnumerable<ClasePrestador>? tipoPrestador = await _tipoPrestadorRepository.GetAllAsync(filter, args);

        if (tipoPrestador is null || !tipoPrestador.Any())
            return Result<IEnumerable<ClasePrestadorDto>?>.Failure("No hay datos para el filtro proporcionado");

        var tipoPrestadorDtos = tipoPrestador.Select(tipoPrestador => _mapper.Map<ClasePrestadorDto>(tipoPrestador)).ToList();

        return Result<IEnumerable<ClasePrestadorDto>?>.Success(tipoPrestadorDtos);
    }

    private Func<ClasePrestador, bool> BuildFilter(string? id, string? nombre)
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
