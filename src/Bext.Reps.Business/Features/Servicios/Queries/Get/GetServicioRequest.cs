using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Servicios.Queries.Get;
public class GetServicioRequest : IRequest<Result<ServicioDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetServicioServicioHandler : IRequestHandler<GetServicioRequest, Result<ServicioDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Servicio, int> _servicioRepository;
    public GetServicioServicioHandler(IReadOnlyRepository<Servicio, int> servicioRepository, IMapper mapper)
    {
        _servicioRepository = servicioRepository;
        _mapper = mapper;
    }

    public async Task<Result<ServicioDto?>> Handle(GetServicioRequest request, CancellationToken cancellationToken)
    {
        var servicio = await _servicioRepository.GetByIdAsync(request.Id, (Servicio b) => b.GrupoServicio);

        if (servicio == null)
        {
            return Result<ServicioDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<ServicioDto?>.Success(_mapper.Map<ServicioDto>(servicio));
    }
}
