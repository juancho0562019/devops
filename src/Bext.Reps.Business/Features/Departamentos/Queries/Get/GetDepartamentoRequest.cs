
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Services;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Entities.DirectorioGeneral;
using MediatR;

namespace Bext.Reps.Business.Features.Departamentos.Queries.Get;
public class GetDepartamentosQuery : IRequest<Result<DepartamentoDto?>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Id { get; set; } = string.Empty;
}
public class GetDepartamentosQueryHandler : IRequestHandler<GetDepartamentosQuery, Result<DepartamentoDto?>>
{
    private readonly IDirectorioGeneralRepository _repository;
    private readonly IMapper _mapper;

    public GetDepartamentosQueryHandler(IDirectorioGeneralRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DepartamentoDto?>> Handle(GetDepartamentosQuery request, CancellationToken cancellationToken)
    {
        var departamento = await _repository.GetDepartamentosByIdAsync(request.Id);

        if (departamento is null)
            return Result<DepartamentoDto?>.Failure($"No se encontraron datos con el codigo {request.Id}");

        var departamentoDto = _mapper.Map<DepartamentoDto>(departamento);
        return Result<DepartamentoDto?>.Success(departamentoDto);
    }
}
