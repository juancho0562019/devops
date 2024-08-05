using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Criterios.Queries.Get;
public class GetCriterioRequest : IRequest<Result<CriterioDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetCriterioHandler : IRequestHandler<GetCriterioRequest, Result<CriterioDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Criterio, int> _criterioRepository;
    public GetCriterioHandler(IReadOnlyRepository<Criterio, int> criterioRepository, IMapper mapper)
    {
        _criterioRepository = criterioRepository;
        _mapper = mapper;
    }

    public async Task<Result<CriterioDto?>> Handle(GetCriterioRequest request, CancellationToken cancellationToken)
    {
        var criterio = await _criterioRepository.GetByIdAsync(request.Id);

        if (criterio == null)
        {
            return Result<CriterioDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<CriterioDto?>.Success(_mapper.Map<CriterioDto>(criterio));
    }
}
