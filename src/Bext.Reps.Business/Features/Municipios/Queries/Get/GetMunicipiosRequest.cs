using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Features.Departamentos;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Entities.DirectorioGeneral;
using MediatR;

namespace Bext.Reps.Business.Features.Municipios.Queries.Get;
public class GetMunicipiosQuery : IRequest<Result<MunicipioDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}

public class GetMunicipiosQueryHandler : IRequestHandler<GetMunicipiosQuery, Result<MunicipioDto?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;
    public GetMunicipiosQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<MunicipioDto?>> Handle(GetMunicipiosQuery request, CancellationToken cancellationToken)
    {
        var vm = await _repository.GetMunicipiosByIdAsync(request.Id);

        if (vm is null)
            return Result<MunicipioDto?>.Failure($"No se encontraron datos con el codigo {request.Id}");
        
        var departamentoDto = _mapper.Map<MunicipioDto>(vm);

        return Result<MunicipioDto?>.Success(departamentoDto);
    }
}
