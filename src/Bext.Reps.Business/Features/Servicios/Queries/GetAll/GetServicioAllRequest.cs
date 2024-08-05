
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Servicios.Queries.GetAll;
public class GetAllServicioRequest : IRequest<Result<IEnumerable<ServicioDto>?>>
{
    public int? Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int GrupoId { get; set; }


}

public class GetAllServicioRequestHandler : IRequestHandler<GetAllServicioRequest, Result<IEnumerable<ServicioDto>?>>
{

    private readonly IReadOnlyRepository<Servicio, int> _servicioRepository;
    private readonly IMapper _mapper;
    public GetAllServicioRequestHandler(IReadOnlyRepository<Servicio, int> servicioRepository, IMapper mapper)
    {
        _servicioRepository = servicioRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<ServicioDto>?>> Handle(GetAllServicioRequest request, CancellationToken cancellationToken)
    {
        Func<Servicio, bool> filter = BuildFilter(request.Id, request.Nombre, request.GrupoId);
        
        object[] args = { request.Id, request.Nombre, request.GrupoId };

        IEnumerable<Servicio>? servicios = await _servicioRepository.GetAllAsync(filter, args, (Servicio b) => b.GrupoServicio);

        if(servicios is null || !servicios.Any())
            return Result<IEnumerable<ServicioDto>?>.Failure("No hay datos para el filtro proporcionado");

        var serviciosDto = servicios.Select(servicio => _mapper.Map<ServicioDto>(servicio)).ToList();

        return Result<IEnumerable<ServicioDto>?>.Success(serviciosDto);
    }


    private Func<Servicio, bool> BuildFilter(int? id, string? nombre, int grupoServicioId)
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
            
            matches &= x.GrupoServicioId == grupoServicioId;
            
            return matches;
        };
    }
}
