using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using MediatR;

namespace Bext.Reps.Business.Features.TipoIdentidades.Queries.Get;
public class GetTipoIdentidadRequest : IRequest<Result<TipoIdentidadDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;


}

public class GetTipoIdentidadHandler : IRequestHandler<GetTipoIdentidadRequest, Result<TipoIdentidadDto?>>
{

    private readonly IReadOnlyRepository<TipoIdentidad, string> _tipoIdentidadRepository;
    private readonly IMapper _mapper;
    public GetTipoIdentidadHandler(IReadOnlyRepository<TipoIdentidad, string> tipoIdentidadRepository, IMapper mapper)
    {
        _tipoIdentidadRepository = tipoIdentidadRepository;
        _mapper = mapper;
    }

    public async Task<Result<TipoIdentidadDto?>> Handle(GetTipoIdentidadRequest request, CancellationToken cancellationToken)
    {
        var tipoIdentidad = await _tipoIdentidadRepository.GetByIdAsync(request.Id);

        if (tipoIdentidad == null)
        {
            return Result<TipoIdentidadDto>.Failure(DefaultMessage.NotFound);
        }
        return Result<TipoIdentidadDto?>.Success(_mapper.Map<TipoIdentidadDto>(tipoIdentidad));

    }
}
