using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Estandares.Queries.Get;
public class GetEstandarRequest : IRequest<Result<EstandarDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetEstandarHandler : IRequestHandler<GetEstandarRequest, Result<EstandarDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Estandar, int> _estandarRepository;
    public GetEstandarHandler(IReadOnlyRepository<Estandar, int> estandarRepository, IMapper mapper)
    {
        _estandarRepository = estandarRepository;
        _mapper = mapper;
    }

    public async Task<Result<EstandarDto?>> Handle(GetEstandarRequest request, CancellationToken cancellationToken)
    {
        var grupoServicio = await _estandarRepository.GetByIdAsync(request.Id);

        if (grupoServicio == null)
        {
            return Result<EstandarDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<EstandarDto?>.Success(_mapper.Map<EstandarDto>(grupoServicio));
    }
}
