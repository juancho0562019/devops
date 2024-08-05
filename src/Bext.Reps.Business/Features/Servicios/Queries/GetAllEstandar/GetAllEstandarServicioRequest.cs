
using AutoMapper;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Criterios;
using Bext.Reps.Business.Features.GrupoServicios;
using Bext.Reps.Business.Features.Servicios;
using Bext.Reps.Business.Features.Servicios.Queries.GetAllEstandar;
using Bext.Reps.Business.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Business.Features.Estandares.Queries.GetAll;
public class GetAllEstandarServicioRequest : IRequest<Result<IEnumerable<EstandarServicioDto>?>>
{
    public int ServicioId { get; set; }

}

public class GetAllEstandarServicioHandler : IRequestHandler<GetAllEstandarServicioRequest, Result<IEnumerable<EstandarServicioDto>?>>
{

    private readonly IReadOnlyRepository<Servicio, int> _servicioRepository;
    private readonly IMapper _mapper;
    private readonly IRepsDbContext _context;
    public GetAllEstandarServicioHandler(IReadOnlyRepository<Servicio, int> servicioRepository, IMapper mapper, IRepsDbContext context)
    {
        _servicioRepository = servicioRepository;
        _mapper = mapper;
        _context = context;
    }


    public async Task<Result<IEnumerable<EstandarServicioDto>?>> Handle(GetAllEstandarServicioRequest request, CancellationToken cancellationToken)
    {

        var serviciosDto = await _context.Servicios
                        .Where(v => v.Id == request.ServicioId)
                        .Select(s => new EstandarServicioDto
                        {
                            Id = s.Id,
                            Nombre = s.Nombre,
                            GrupoServicio = s.GrupoServicio != null ? new GrupoServicioDto { Id = s.GrupoServicio.Id, Nombre = s.GrupoServicio.Nombre } : new GrupoServicioDto(),
                            Estandares = s.Estandares.Select(e => new EstandarPorServicioDto
                            {
                                Estandar = new EstandarDto {
                                                    Id = e.Estandar.Id,
                                                    Nombre = e.Estandar.Nombre,
                                                    Criterios = e.Estandar.Criterios.Select(v => new CriterioDto { 
                                                        Id = v.Id,
                                                        Nombre = v.Nombre
                                                    }).ToList()
                                           }
                            }).ToList()
                        })
                        .ToListAsync(cancellationToken);

       

        return Result<IEnumerable<EstandarServicioDto>?>.Success(serviciosDto);
    }

}
