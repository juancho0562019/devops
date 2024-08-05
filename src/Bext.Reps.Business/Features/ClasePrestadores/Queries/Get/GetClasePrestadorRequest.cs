using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.ClasePrestadores.Queries.Get;
public class GetClasePrestadorRequest : IRequest<Result<ClasePrestadorDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}

public class GetClasePrestadorHandler : IRequestHandler<GetClasePrestadorRequest, Result<ClasePrestadorDto?>>
{

    private readonly IReadOnlyRepository<ClasePrestador, string> _tipoPrestadorRepository;
    private readonly IMapper _mapper;
    public GetClasePrestadorHandler(IReadOnlyRepository<ClasePrestador, string> tipoPrestadorRepository, IMapper mapper)
    {
        _tipoPrestadorRepository = tipoPrestadorRepository;
        _mapper = mapper;
    }

    public async Task<Result<ClasePrestadorDto?>> Handle(GetClasePrestadorRequest request, CancellationToken cancellationToken)
    {

        var tipoPrestador = await _tipoPrestadorRepository.GetByIdAsync(request.Id);

        if (tipoPrestador == null)
        {
            return Result<ClasePrestadorDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<ClasePrestadorDto?>.Success(_mapper.Map<ClasePrestadorDto>(tipoPrestador));

    }

 
}
