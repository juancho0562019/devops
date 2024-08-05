using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.GrupoServicios.Queries.Get;
public class GetGrupoServicioRequest : IRequest<Result<GrupoServicioDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetGrupoServicioHandler : IRequestHandler<GetGrupoServicioRequest, Result<GrupoServicioDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<GrupoServicio, int> _grupoServicioRepository;
    public GetGrupoServicioHandler(IReadOnlyRepository<GrupoServicio, int> grupoServicioRepository, IMapper mapper)
    {
        _grupoServicioRepository = grupoServicioRepository;
        _mapper = mapper;
    }

    public async Task<Result<GrupoServicioDto?>> Handle(GetGrupoServicioRequest request, CancellationToken cancellationToken)
    {
        var grupoServicio = await _grupoServicioRepository.GetByIdAsync(request.Id);

        if (grupoServicio == null)
        {
            return Result<GrupoServicioDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<GrupoServicioDto?>.Success(_mapper.Map<GrupoServicioDto>(grupoServicio));
    }
}
