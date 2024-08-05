
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Especificidades.Queries.GetAll;
public class GetAllEspecificidadRequest : IRequest<Result<IEnumerable<EspecificidadDto>?>>
{
    public int? Id { get; set; }

    public string Nombre { get; set; } = string.Empty;


}

public class GetAllEspecificidadHandler : IRequestHandler<GetAllEspecificidadRequest, Result<IEnumerable<EspecificidadDto>?>>
{

    private readonly IReadOnlyRepository<Especificidad, int> _especificidadRepository;
    private readonly IMapper _mapper;
    public GetAllEspecificidadHandler(IReadOnlyRepository<Especificidad, int> especificidadRepository, IMapper mapper)
    {
        _especificidadRepository = especificidadRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<EspecificidadDto>?>> Handle(GetAllEspecificidadRequest request, CancellationToken cancellationToken)
    {
        Func<Especificidad, bool> filter = BuildFilter(request.Id, request.Nombre);
        
        object[] args = { request.Id ?? 0, request.Nombre };
        
        IEnumerable<Especificidad>? especificidades = await _especificidadRepository.GetAllAsync(filter, args);

        if(especificidades is null || !especificidades.Any())
            return Result<IEnumerable<EspecificidadDto>?>.Failure("No hay datos para el filtro proporcionado");

        var especificidadesDto = especificidades.Select(especificidad => _mapper.Map<EspecificidadDto>(especificidad)).ToList();

        return Result<IEnumerable<EspecificidadDto>?>.Success(especificidadesDto);
    }


    private Func<Especificidad, bool> BuildFilter(int? id, string? nombre)
    {
        return x =>
        {
            bool matches = true;
            if (id.HasValue && id > 0)
            {
                matches &= x.Id == id.Value;
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                matches &= x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase);
            }
           
            return matches;
        };
    }
}
