
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using MediatR;

namespace Bext.Reps.Business.Features.Modalidades.Queries.GetAll;
public class GetAllModalidadRequest : IRequest<Result<IEnumerable<ModalidadDto>?>>
{
    public int? Id { get; set; }

    public string? Nombre { get; set; } = string.Empty;

}

public class GetTipoPersonaAllRequestHandler : IRequestHandler<GetAllModalidadRequest, Result<IEnumerable<ModalidadDto>?>>
{

    private readonly IReadOnlyRepository<Modalidad, int> _modalidadRepository;
    private readonly IMapper _mapper;
    public GetTipoPersonaAllRequestHandler(IReadOnlyRepository<Modalidad, int> modalidadRepository, IMapper mapper)
    {
        _modalidadRepository = modalidadRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<ModalidadDto>?>> Handle(GetAllModalidadRequest request, CancellationToken cancellationToken)
    {
        Func<Modalidad, bool> filter = BuildFilter(request.Id, request.Nombre);
        object[] args = { request?.Id ?? 0, request?.Nombre ?? "" };
        IEnumerable<Modalidad>? modalidades = await _modalidadRepository.GetAllAsync(filter, args);

        if (modalidades is null || !modalidades.Any())
            return Result<IEnumerable<ModalidadDto>?>.Failure("No hay datos para el filtro proporcionado");

        var modalidadDtos = modalidades.Select(modalidad => _mapper.Map<ModalidadDto>(modalidad)).ToList();

        return Result<IEnumerable<ModalidadDto>?>.Success(modalidadDtos);
    }


    private Func<Modalidad, bool> BuildFilter(int? id, string? nombre)
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
