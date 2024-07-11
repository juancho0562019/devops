using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bext.Reps.Business.Data;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Business.Features.Terceros.Queries.Get;
public class GetTerceroRequest : IRequest<TerceroDto>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string NumeroDocumento { get; set; } = string.Empty;
}

public class GetTerceroHandler : IRequestHandler<GetTerceroRequest, TerceroDto>
{
    private readonly IRepsDbContext _context;
    private readonly IMapper _mapper;
    public GetTerceroHandler(IRepsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TerceroDto> Handle(GetTerceroRequest request, CancellationToken cancellationToken)
    {
        var vm = await _context.Terceros
                 .AsNoTracking()
                 .Where(e => e.Identificacion.NumeroDocumento == request.NumeroDocumento)
                 .ProjectTo<TerceroDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

        if (vm is null)
            throw new NotFoundException("Tercero", request.NumeroDocumento);

        return vm;
    }
}
