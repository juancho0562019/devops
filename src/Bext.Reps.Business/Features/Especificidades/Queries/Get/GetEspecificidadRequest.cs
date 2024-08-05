using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Especificidades.Queries.Get;
public class GetEspecificidadRequest : IRequest<Result<EspecificidadDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetEspecificidadHandler : IRequestHandler<GetEspecificidadRequest, Result<EspecificidadDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Especificidad, int> _especificidadRepository;
    public GetEspecificidadHandler(IReadOnlyRepository<Especificidad, int> especificidadRepository, IMapper mapper)
    {
        _especificidadRepository = especificidadRepository;
        _mapper = mapper;
    }

    public async Task<Result<EspecificidadDto?>> Handle(GetEspecificidadRequest request, CancellationToken cancellationToken)
    {
        var especificidades = await _especificidadRepository.GetByIdAsync(request.Id);

        if (especificidades == null)
        {
            return Result<EspecificidadDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<EspecificidadDto?>.Success(_mapper.Map<EspecificidadDto>(especificidades));
    }
}
