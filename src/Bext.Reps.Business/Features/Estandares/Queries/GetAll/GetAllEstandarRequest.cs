
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Estandares.Queries.GetAll;
public class GetAllEstandarRequest : IRequest<Result<IEnumerable<EstandarDto>?>>
{
    public int? Id { get; set; }

    public string Nombre { get; set; } = string.Empty;


}

public class GetAllEstandarHandler : IRequestHandler<GetAllEstandarRequest, Result<IEnumerable<EstandarDto>?>>
{

    private readonly IReadOnlyRepository<Estandar, int> _estandarRepository;
    private readonly IMapper _mapper;
    public GetAllEstandarHandler(IReadOnlyRepository<Estandar, int> estandarRepository, IMapper mapper)
    {
        _estandarRepository = estandarRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<EstandarDto>?>> Handle(GetAllEstandarRequest request, CancellationToken cancellationToken)
    {
        Func<Estandar, bool> filter = BuildFilter(request.Id, request.Nombre);
        
        object[] args = { request.Id ?? 0, request.Nombre };
        
        IEnumerable<Estandar>? estandares = await _estandarRepository.GetAllAsync(filter, args, (Estandar b) => b.Criterios);

        if(estandares is null || !estandares.Any())
            return Result<IEnumerable<EstandarDto>?>.Failure("No hay datos para el filtro proporcionado");

        var estandarDto = estandares.Select(estandar => _mapper.Map<EstandarDto>(estandar)).ToList();

        return Result<IEnumerable<EstandarDto>?>.Success(estandarDto);
    }


    private Func<Estandar, bool> BuildFilter(int? id, string? nombre)
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
