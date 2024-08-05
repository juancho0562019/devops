using System.ComponentModel.DataAnnotations;
using Bext.Reps.Business.Commons.Exceptions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Commons.Extensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios;
public class AgregarServiciosRequest : IRequest<Result<int>>
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int EntidadId { get; set; }
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int SedeId { get; set; }
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public ServicioRequest Servicio { get; set; } = null!;
}

public class ServicioRequest
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int ServicioId { get; set; }

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int ComplejidadServicioId { get; set; }

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public List<AutoEvaluacionRequest> AutoEvaluacion { get; set; } = null!;

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public List<int> Especificidades { get; set; } = new List<int>();

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public List<CapacidadInstaladaRequest> CapacidadInstalada { get; set; } = [];

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public List<FranjaHorariaRequest> FranjasHorarias { get; set; } = new List<FranjaHorariaRequest>();
}
public class FranjaHorariaRequest
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public TimeSpan HoraApertura { get; set; }

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public TimeSpan HoraCierre { get; set; }

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public List<DayOfWeek> DiasAtencion { get; set; } = new List<DayOfWeek>();
}

public class AutoEvaluacionRequest
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int CriterioId { get; set; }
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public bool Cumple { get; set; }
}
public class CapacidadInstaladaRequest 
{
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public TipoRecurso TipoRecurso { get; set; }
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public int Capacidad { get; set; }
}

public class AgregarServiciosHandler : IRequestHandler<AgregarServiciosRequest, Result<int>>
{
    private readonly IRepsDbContext _context;
    private readonly IValidator<AgregarServiciosRequest> _validator;
    private readonly IReadOnlyRepository<Servicio, int> _servicioRepository;
    private readonly IReadOnlyRepository<Criterio, int> _criterioRepository;

    public AgregarServiciosHandler(
        IRepsDbContext context,
        IValidator<AgregarServiciosRequest> validator,
        IReadOnlyRepository<Servicio, int> servicioRepository,
        IReadOnlyRepository<Criterio, int> criterioRepository)
    {
        _context = context;
        _validator = validator;
        _servicioRepository = servicioRepository;
        _criterioRepository = criterioRepository;
    }

    public async Task<Result<int>> Handle(AgregarServiciosRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors);

        if (!await ValidarSede(request.SedeId, request.EntidadId, cancellationToken))
            return Result<int>.Failure("La sede no corresponde a la entidad enviada");

        var solicitud = Solicitud.CrearSolicitudHabilitacion(0, request.EntidadId, DateTime.UtcNow);

        if (!await ValidarEstandares(request.Servicio, cancellationToken))
            return Result<int>.Failure("No se enviaron evaluados todos los estandares obligatorios para el servicio");

        var servicioEntity = await _servicioRepository.GetByIdAsync(request.Servicio.ServicioId);
        servicioEntity = servicioEntity.ValidateNull();

        var evaluacion = EvaluacionServicio.CrearEvaluacionAuto();
        
        var capacidades = request.Servicio.CapacidadInstalada.Select(v => CapacidadInstalada.Create(v.TipoRecurso, v.Capacidad));

        foreach (var estandarEvaluacion in request.Servicio.AutoEvaluacion)
        {
            await ValidarYAgregarEvaluacion(evaluacion, estandarEvaluacion, cancellationToken);
        }

        var franjasHorarias = request.Servicio.FranjasHorarias.Select(f => FranjaHoraria.Create(
            f.HoraApertura,
            f.HoraCierre,
            f.DiasAtencion
        )).ToList();

        var servicioSede = ServicioInscritoSede.Create(request.SedeId, servicioEntity.GrupoServicioId, request.Servicio.ServicioId, capacidades.ToList(), evaluacion, franjasHorarias);

        solicitud.AddServicio(servicioSede);

        await _context.Solicitudes.AddAsync(solicitud, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(solicitud.Id);
    }

    private async Task ValidarYAgregarEvaluacion(EvaluacionServicio evaluacion, AutoEvaluacionRequest estandarEvaluacion, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var criterio = await _criterioRepository.GetByIdAsync(estandarEvaluacion.CriterioId);

        criterio = criterio.ValidateNull(nameof(Criterio), $"El criterio {estandarEvaluacion.CriterioId} no existe");

        evaluacion.AddDetalle(new DetalleEvaluacionServicio
        {
            EstandarId = criterio.EstandarId,
            CriterioId = criterio.Id,
            Cumple = estandarEvaluacion.Cumple
        });
    }

    private async Task<bool> ValidarEstandares(ServicioRequest servicioRequest, CancellationToken cancellationToken)
    {
      
        var estandaresParametrizados = await _context.EstandarPorServicios
            .Include(e => e.Estandar)
            .ThenInclude(e => e.Criterios)
            .Where(e => e.ServicioId == servicioRequest.ServicioId)
            .SelectMany(e => e.Estandar.Criterios)
            .ToListAsync(cancellationToken);

        if (!estandaresParametrizados.Any())
            return true;

        var estandaresParametrizadosIds = estandaresParametrizados.Select(c => c.Id).ToList();

        
        var estandaresEnviadosIds = servicioRequest.AutoEvaluacion.Select(a => a.CriterioId).ToList();

      
        return estandaresParametrizadosIds.Count <= estandaresEnviadosIds.Count &&
               !estandaresParametrizadosIds.Except(estandaresEnviadosIds).Any();
    }

    private async Task<bool> ValidarSede(int sedeId, int entidadId, CancellationToken cancellationToken)
    {
        return await _context.Sedes.AnyAsync(e => e.Id == sedeId && e.EntidadId == entidadId, cancellationToken);
    }
}
