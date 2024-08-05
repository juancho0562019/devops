using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;

namespace Bext.Reps.Business.Features.Modalidades.Queries.Get;
public class GetModalidadRequest : IRequest<Result<ModalidadDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Id { get; set; }
}

public class GetModalidadHandler : IRequestHandler<GetModalidadRequest, Result<ModalidadDto?>>
{

    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Modalidad, int> _modalidadRepository;
    public GetModalidadHandler(IReadOnlyRepository<Modalidad, int> modalidadRepository, IMapper mapper)
    {
        _modalidadRepository = modalidadRepository;
        _mapper = mapper;
    }

    public async Task<Result<ModalidadDto?>> Handle(GetModalidadRequest request, CancellationToken cancellationToken)
    {
        var grupoServicio = await _modalidadRepository.GetByIdAsync(request.Id);

        if (grupoServicio == null)
        {
            return Result<ModalidadDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<ModalidadDto?>.Success(_mapper.Map<ModalidadDto>(grupoServicio));
    }
}
