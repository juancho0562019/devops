using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using MediatR;

namespace Bext.Reps.Business.Features.TiposDocumentos.Queries.GetAll;
public class GetAllTipoDocumentoRequest : IRequest<Result<IEnumerable<TipoDocumentoDto>?>>
{
    public int? Id { get; set; }

    public string? Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public TipoDocumentoPrestador Tipo { get; set; }
}

public class GetAllTipoDocumentoHandler : IRequestHandler<GetAllTipoDocumentoRequest, Result<IEnumerable<TipoDocumentoDto>?>>
{

    private readonly IReadOnlyRepository<TipoDocumento, int> _tipoDocumentoRepository;
    private readonly IMapper _mapper;
    public GetAllTipoDocumentoHandler(IReadOnlyRepository<TipoDocumento, int> tipoDocumentoRepository, IMapper mapper)
    {
        _tipoDocumentoRepository = tipoDocumentoRepository;
        _mapper = mapper;
    }


    public async Task<Result<IEnumerable<TipoDocumentoDto>?>> Handle(GetAllTipoDocumentoRequest request, CancellationToken cancellationToken)
    {
        Func<TipoDocumento, bool> filter = BuildFilter(request.Id, request.Nombre, request.Tipo);
        object[] args = { request.Id??0, request.Nombre??"", request.Tipo };
        IEnumerable<TipoDocumento>? tipoDocumentos = await _tipoDocumentoRepository.GetAllAsync(filter, args);

        if (tipoDocumentos is null || !tipoDocumentos.Any())
            return Result<IEnumerable<TipoDocumentoDto>?>.Failure("No hay datos para el filtro proporcionado");

        var tipoDocumentosDto = tipoDocumentos.Select(tipoDocumento => _mapper.Map<TipoDocumentoDto>(tipoDocumento)).ToList();

        return Result<IEnumerable<TipoDocumentoDto>?>.Success(tipoDocumentosDto);
    }

    private Func<TipoDocumento, bool> BuildFilter(int? id, string? nombre, TipoDocumentoPrestador tipo)
    {
        return x =>
        {
            bool matches = true;
            if (id.HasValue && id > 0)
            {
                matches &= x.Id == id.Value;
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                matches &= x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase);
            }
            
            matches &= x.Tipo == tipo;
            
            return matches;
        };
    }
}
