using Bext.Reps.Business.Commons.Exceptions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Entidades.Commands.AgregarSede.Validators;
using Bext.Reps.Business.Features.Entidades.Commands.Commons.Request;
using Bext.Reps.Business.Features.Entidades.Commands.Create;
using Bext.Reps.Business.Models;
using FluentValidation;
using MediatR;

namespace Bext.Reps.Business.Features.Entidades.Commands.AgregarSede;
public class AgregarSedeRequest : BaseSedeRequest, IRequest<Result<int>>
{
    
    public int EntidadId { get; set; }
}

public class AgregarSedeCommandHandler : IRequestHandler<AgregarSedeRequest, Result<int>>
{
    private readonly IRepsDbContext _context;
    private readonly IValidator<AgregarSedeRequest> _validator;
    private readonly IEntidadRepository _entidadRepository;
    public AgregarSedeCommandHandler(IRepsDbContext context, IValidator<AgregarSedeRequest> validator, IEntidadRepository entidadRepository)
    {
        _context = context;
        _validator = validator;
        _entidadRepository = entidadRepository;
    }


    public async Task<Result<int>> Handle(AgregarSedeRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors);

        var entidad = await _entidadRepository.GetByIdAsync(request.EntidadId);
        if (entidad == null)
        {
            return Result<int>.Failure("Entidad no encontrada.");
        }

        if (request.EsPrincipal) 
        {
            var sedePrincipal = await _entidadRepository.GetSedePrincipalByIdAsync(request.EntidadId);
            if (sedePrincipal != null)
            {
                return Result<int>.Failure("Ya existe una sede Principal para esta entidad.");
            }
        }
        

        var ubicacionSede = Ubicacion.Crear(request.Departamento,
                                               request.Municipio,
                                               request.Direccion);

        var datosContacto = Contacto.Crear(request.TelefonoFijo,
                                           request.TelefonoMovil,
                                           request.TelefonoFax,
                                           null,
                                           request.Email);
        var sede = new Sede.Builder()
                  .ConNombreResponsable(request.NombreResponsable)
                  .ConNombre(request.NombreSede)
                  .ConEsPrincipal(request.EsPrincipal)
                  .ConUbicacion(ubicacionSede)
                  .ConContacto(datosContacto)
                  .ConZona(request.Zona)
                  .ConCentroPoblado(request.CentroPoblado)
                  .ConBarrio(request.Barrio)
                  .Build();

        entidad.AddSede(sede);

        _context.Entidades.Update(entidad);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(entidad.Id);
    }
}
