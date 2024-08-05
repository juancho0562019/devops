
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.GrupoServicios.Queries.GetAll;
public class GetAllGrupoServiciosRequest : IRequest<Result<IEnumerable<GrupoServicioDto>?>>
{
    public int? Id { get; set; }

    public string? Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int ModalidadId { get; set; }
}

public class GetTipoPersonaAllRequestHandler : IRequestHandler<GetAllGrupoServiciosRequest, Result<IEnumerable<GrupoServicioDto>?>>
{

    private readonly IReadOnlyRepository<GrupoServicio, int> _grupoServicioRepository;
    private readonly IMapper _mapper;
    public GetTipoPersonaAllRequestHandler(IReadOnlyRepository<GrupoServicio, int> grupoServicioRepository, IMapper mapper)
    {
        _grupoServicioRepository = grupoServicioRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<GrupoServicioDto>?>> Handle(GetAllGrupoServiciosRequest request, CancellationToken cancellationToken)
    {
        Func<GrupoServicio, bool> filter = BuildFilter(request.Id, request.Nombre, request.ModalidadId);
        object[] args = { request.Id ?? 0, request.Nombre ?? "", request.ModalidadId };
        IEnumerable<GrupoServicio>? grupoServicios = await _grupoServicioRepository.GetAllAsync(filter, args);

        if (grupoServicios is null || !grupoServicios.Any())
            return Result<IEnumerable<GrupoServicioDto>?>.Failure("No hay datos para el filtro proporcionado");

        var grupoServicioDtos = grupoServicios.Select(grupoServicio => _mapper.Map<GrupoServicioDto>(grupoServicio)).ToList();

        return Result<IEnumerable<GrupoServicioDto>?>.Success(grupoServicioDtos);
    }


    private Func<GrupoServicio, bool> BuildFilter(int? id, string? nombre, int modalidad)
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
           
                matches &= x.ModalidadId == modalidad;
           
            return matches;
        };
    }
}
