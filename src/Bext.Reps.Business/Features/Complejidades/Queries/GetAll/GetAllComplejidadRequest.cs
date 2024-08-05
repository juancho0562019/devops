
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Complejidades.Queries.GetAll;
public class GetAllComplejidadRequest : IRequest<Result<IEnumerable<ComplejidadDto>?>>
{
    public int? Id { get; set; }

    public string Nivel { get; set; } = string.Empty;


}

public class GetAllComlplejidadHandler : IRequestHandler<GetAllComplejidadRequest, Result<IEnumerable<ComplejidadDto>?>>
{

    private readonly IReadOnlyRepository<Complejidad, int> _complejidadRepository;
    private readonly IMapper _mapper;
    public GetAllComlplejidadHandler(IReadOnlyRepository<Complejidad, int> complejidadRepository, IMapper mapper)
    {
        _complejidadRepository = complejidadRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<ComplejidadDto>?>> Handle(GetAllComplejidadRequest request, CancellationToken cancellationToken)
    {
        Func<Complejidad, bool> filter = BuildFilter(request.Id, request.Nivel);
        
        object[] args = { request.Id ?? 0, request.Nivel };
        
        IEnumerable<Complejidad>? complejidades = await _complejidadRepository.GetAllAsync(filter, args);

        if(complejidades is null || !complejidades.Any())
            return Result<IEnumerable<ComplejidadDto>?>.Failure("No hay datos para el filtro proporcionado");

        var complejidadesDto = complejidades.Select(complejidad => _mapper.Map<ComplejidadDto>(complejidad)).ToList();

        return Result<IEnumerable<ComplejidadDto>?>.Success(complejidadesDto);
    }


    private Func<Complejidad, bool> BuildFilter(int? id, string? nivel)
    {
        return x =>
        {
            bool matches = true;
            if (id.HasValue && id > 0)
            {
                matches &= x.Id == id.Value;
            }
            if (!string.IsNullOrEmpty(nivel))
            {
                matches &= x.Nivel.Contains(nivel, StringComparison.OrdinalIgnoreCase);
            }
           
            return matches;
        };
    }
}
