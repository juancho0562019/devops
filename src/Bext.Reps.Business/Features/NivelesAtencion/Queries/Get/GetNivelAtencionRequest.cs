using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.NivelesAtencion.Queries.Get;
public class GetNivelAtencionRequest : IRequest<Result<NivelAtencionDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetNivelAtencionQueryHandler : IRequestHandler<GetNivelAtencionRequest, Result<NivelAtencionDto?>>
{
    private readonly IReadOnlyRepository<NivelAtencion, int> _repository;
    private readonly IMapper _mapper;
    public GetNivelAtencionQueryHandler(IReadOnlyRepository<NivelAtencion, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<NivelAtencionDto?>> Handle(GetNivelAtencionRequest request, CancellationToken cancellationToken)
    {
        var vm = await _repository.GetByIdAsync(request.Id);

        if (vm is null)
            return Result<NivelAtencionDto?>.Failure($"No se encontraron datos con el codigo {request.Id}");

        var municipioDto = _mapper.Map<NivelAtencionDto>(vm);

        return Result<NivelAtencionDto?>.Success(municipioDto);
    }
}
