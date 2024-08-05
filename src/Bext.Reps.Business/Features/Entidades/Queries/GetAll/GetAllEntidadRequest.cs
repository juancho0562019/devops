using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bext.Reps.Business.Commons.Extensions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Business.Features.Entidades.Queries.GetAll;
public class GetAllEntidadRequest : IRequest<PaginatedList<EntidadDto>>
{
    public string? ClasePrestadorId { get; set; }
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string TipoPersonaId { get; set; } = string.Empty;
    public string? MunicipioId { get; set; }
    public string? Correo { get; set; }
    public string? EstadoSolicitud { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAllEntidadHandler : IRequestHandler<GetAllEntidadRequest, PaginatedList<EntidadDto>>
{
    private readonly IRepsDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEntidadHandler(IRepsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<EntidadDto>> Handle(GetAllEntidadRequest request, CancellationToken cancellationToken)
    {
        var query = _context.Entidades
                            .Include(v => v.Tercero)
                            .Include(v => v.Periodos)
                            .ThenInclude(v => v.Contacto)
                            .AsQueryable();

        if (!string.IsNullOrEmpty(request.TipoPersonaId))
        {
            query = query.Where(v => v.TipoPersonaId.Equals(request.TipoPersonaId));
        }

        if (!string.IsNullOrEmpty(request.ClasePrestadorId))
        {
            query = query.Where(v => v.TipoPrestadorId.Equals(request.ClasePrestadorId));
        }

        if (!string.IsNullOrEmpty(request.MunicipioId))
        {
            query = query.Where(v => v.Tercero.Ubicacion.Municipio.Equals(request.MunicipioId));
        }

        PaginatedList<EntidadDto> result;

        if (request.TipoPersonaId == "PN")
        {
                result = await query
               .OrderBy(x => x.Identificacion.NumeroDocumento)
               .ProjectTo<EntidadDto>(_mapper.ConfigurationProvider)
               .Select(entidad => new EntidadDto
               {
                   Tercero = entidad.Tercero,
               }).PaginatedListAsync(request.PageNumber, request.PageSize);            
            
        }
        else if (request.TipoPersonaId == "PJ")
        {
            result = await query
            .OrderBy(x => x.Identificacion.NumeroDocumento)
            .ProjectTo<EntidadDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            throw new ArgumentException("TipoPersonaId inválido");
        }

        return result;
    }
}
