using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.NivelesAtencion.Queries.GetAll;
public class GetAllNivelAtencionRequest : IRequest<Result<IEnumerable<NivelAtencionDto>?>>
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;


}

public class GetAllNivelAtencionHandler : IRequestHandler<GetAllNivelAtencionRequest, Result<IEnumerable<NivelAtencionDto>?>>
{

    private readonly IReadOnlyRepository<NivelAtencion, int> _nivelAtencionRepository;
    private readonly IMapper _mapper;
    public GetAllNivelAtencionHandler(IReadOnlyRepository<NivelAtencion, int> nivelAtencionRepository, IMapper mapper)
    {
        _nivelAtencionRepository = nivelAtencionRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<NivelAtencionDto>?>> Handle(GetAllNivelAtencionRequest request, CancellationToken cancellationToken)
    {
        Func<NivelAtencion, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request.Id, request.Nombre };
        IEnumerable<NivelAtencion>? nivelAtencion = await _nivelAtencionRepository.GetAllAsync(filter, args);

        if (nivelAtencion is null || !nivelAtencion.Any())
            return Result<IEnumerable<NivelAtencionDto>?>.Failure("No hay datos para el filtro proporcionado");

        var nivelAtencionDto = nivelAtencion.Select(nivelAtencion => _mapper.Map<NivelAtencionDto>(nivelAtencion)).ToList();

        return Result<IEnumerable<NivelAtencionDto>?>.Success(nivelAtencionDto);
    }

    private Func<NivelAtencion, bool> BuildFilter(int? id, string? nombre)
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
