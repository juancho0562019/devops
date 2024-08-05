using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Complejidades.Queries.Get;
public class GetComplejidadRequest : IRequest<Result<ComplejidadDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetComplejidadHandler : IRequestHandler<GetComplejidadRequest, Result<ComplejidadDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Complejidad, int> _complejidadRepository;
    public GetComplejidadHandler(IReadOnlyRepository<Complejidad, int> complejidadRepository, IMapper mapper)
    {
        _complejidadRepository = complejidadRepository;
        _mapper = mapper;
    }

    public async Task<Result<ComplejidadDto?>> Handle(GetComplejidadRequest request, CancellationToken cancellationToken)
    {
        var complejidades = await _complejidadRepository.GetByIdAsync(request.Id);

        if (complejidades == null)
        {
            return Result<ComplejidadDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<ComplejidadDto?>.Success(_mapper.Map<ComplejidadDto>(complejidades));
    }
}
