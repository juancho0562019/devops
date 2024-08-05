
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Criterios.Queries.GetAll;
public class GetAllCriterioRequest : IRequest<Result<IEnumerable<CriterioDto>?>>
{
    public int? Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int EstandarId { get; set; }


}

public class GetAllServicioRequestHandler : IRequestHandler<GetAllCriterioRequest, Result<IEnumerable<CriterioDto>?>>
{

    private readonly IReadOnlyRepository<Criterio, int> _criterioRepository;
    private readonly IMapper _mapper;
    public GetAllServicioRequestHandler(IReadOnlyRepository<Criterio, int> criterioRepository, IMapper mapper)
    {
        _criterioRepository = criterioRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<CriterioDto>?>> Handle(GetAllCriterioRequest request, CancellationToken cancellationToken)
    {
        Func<Criterio, bool> filter = BuildFilter(request.Id, request.Nombre, request.EstandarId);
        
        object[] args = { request.Id, request.Nombre, request.EstandarId };

        IEnumerable<Criterio>? estandares = await _criterioRepository.GetAllAsync(filter, args, (Criterio b) => b.Estandar);

        if(estandares is null || !estandares.Any())
            return Result<IEnumerable<CriterioDto>?>.Failure("No hay datos para el filtro proporcionado");

        var estandaresDto = estandares.Select(servicio => _mapper.Map<CriterioDto>(servicio)).ToList();

        return Result<IEnumerable<CriterioDto>?>.Success(estandaresDto);
    }

    private Func<Criterio, bool> BuildFilter(int? id, string? nombre, int estandarId)
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
            
            matches &= x.EstandarId == estandarId;
            
            return matches;
        };
    }
}
