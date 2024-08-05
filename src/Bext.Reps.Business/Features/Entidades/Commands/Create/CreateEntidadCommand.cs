
using System.ComponentModel.DataAnnotations;
using Bext.Reps.Business.Commons.Exceptions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Features.Entidades.Commands.Commons.Request;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using FluentValidation;
using MediatR;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create;
public record CreateEntidadCommand(
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string RazonSocial,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string NumeroIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Direccion,
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TelefonoFijo,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Correo,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string ClasePrestador,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoPersona,
    
    string? TipoNaturaleza,

    string? SubTipoNaturaleza,


    ActaConstitucionRequest? ActaConstitucion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    TerceroPrestadorRequest TerceroPrestadorRequest,
    

    List<RepresentanteRequest>? RepresentanteRequest,
    
    List<SedeRequest>? SedeRequest
) : IRequest<Result<int>>;

public class SedeRequest : BaseSedeRequest { }


 

public class CreateEntidadCommandHandler : IRequestHandler<CreateEntidadCommand, Result<int>>
{
    private readonly IRepsDbContext _context;
    private readonly IValidator<CreateEntidadCommand> _validator;
    public CreateEntidadCommandHandler(IRepsDbContext context, IValidator<CreateEntidadCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateEntidadCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors);
        
        
        var identificacion = Identificacion.Crear(request.TerceroPrestadorRequest.TipoIdentificacion, request.TerceroPrestadorRequest.NumeroIdentificacion, true);

        var direccion = Ubicacion.Crear(request.TerceroPrestadorRequest.Departamento, request.TerceroPrestadorRequest.Municipio,
            request.TerceroPrestadorRequest.Direccion);


        var tercero = new Tercero.Builder(request.TerceroPrestadorRequest.TipoPersona,
                                             identificacion)
                .ConNombres(request.TerceroPrestadorRequest.RazonSocial, request.TerceroPrestadorRequest.PrimerNombre, request.TerceroPrestadorRequest.SegundoNombre, request.TerceroPrestadorRequest.PrimerApellido, request.TerceroPrestadorRequest.SegundoApellido)
                .ConUbicacion(direccion)
                .ConDatosContacto(request.TerceroPrestadorRequest.TelefonoFijo, request.TerceroPrestadorRequest.TelefonoMovil, request.TerceroPrestadorRequest.TelefonoFax, request.TerceroPrestadorRequest.SitioWeb, request.TerceroPrestadorRequest.Email)
                .Build();

       



        var identificacionEntidad = Identificacion.Crear(request.TipoIdentificacion, request.NumeroIdentificacion, true);

        var datosContacto = Contacto.Crear(request.TelefonoFijo, null, null, null, request.Correo);

        var entidadBuilder = new Entidad.Builder(0)
            .ConIdentificacion(identificacionEntidad)
            .ConDatosContacto(datosContacto)
            .ConRazonSocial(request.RazonSocial)
            .ConDireccion(request.Direccion)
            .ConClasePrestador(request.ClasePrestador)
            .ConTipoPersona(request.TipoPersona)
            .ConTipoNaturalezaJuridica(request.TipoNaturaleza, request.SubTipoNaturaleza,request.TipoPersona)
            .ConTercero(tercero);

        ActaConstitucion? actaConstitucion = null;

        if (request.ActaConstitucion != null)
        {
            actaConstitucion = new ActaConstitucion.Builder()
                          .ConCaracterTerritorial(request.ActaConstitucion?.CaracterTerritorial)
                          .ConNivelAtencion(request.ActaConstitucion?.NivelAtencion)
                          .ConEmpresaSocialEstado(request.ActaConstitucion?.EmpresaSocialEstado)
                          .ConActoConstitucion(request.ActaConstitucion?.ActoConstitucion)
                          .ConNumeroActo(request.ActaConstitucion?.NumeroActo)
                          .ConFechaActo(request.ActaConstitucion?.FechaActo)
                          .ConEntidadExpide(request.ActaConstitucion?.EntidadExpide)
                          .ConCiudadExpedicion(request.ActaConstitucion?.CiudadExpedicion)
                          .Build();

            
        }
        var entidad = entidadBuilder.ConActaConstitucion(actaConstitucion).Build();

        if (request.SedeRequest != null && request.SedeRequest.Any()) 
        {
            foreach (var sedeRequest in request.SedeRequest)
            {
                var ubicacionSede = Ubicacion.Crear(sedeRequest.Departamento,
                                               sedeRequest.Municipio,
                                               sedeRequest.Direccion);

                var datosContactoSede = Contacto.Crear(sedeRequest.TelefonoFijo,
                                                   sedeRequest.TelefonoMovil,
                                                   sedeRequest.TelefonoFax,
                                                   null,
                                                   sedeRequest.Email);

                var sede = new Sede.Builder()
                    .ConNombreResponsable(sedeRequest.NombreResponsable)
                    .ConNombre(sedeRequest.NombreSede)
                    .ConEsPrincipal(sedeRequest.EsPrincipal)
                    .ConUbicacion(ubicacionSede)
                    .ConContacto(datosContactoSede)
                    .ConZona(sedeRequest.Zona)
                    .ConCentroPoblado(sedeRequest.CentroPoblado)
                    .ConBarrio(sedeRequest.Barrio)
                    .Build();

                entidad.AddSede(sede);
            }
           
        }


        if (request.RepresentanteRequest != null && request.RepresentanteRequest.Any())
        {
            foreach (var representante in request.RepresentanteRequest)
            {
                var periodoRepresentacion = new PeriodoRepresentacion.Builder()
                                                         .ConNombres(representante.PrimerNombre,
                                                                     representante.SegundoNombre,
                                                                     representante.PrimerApellido,
                                                                     representante.SegundoApellido)
                                                         .ConIdentificacion(representante.TipoIdentificacion,
                                                                            representante.NumeroIdentificacion)
                                                         .ConPeriodoRepresentacion(representante.TipoRepresentacion, representante.FechaInicioRepresentacion)
                                                         .Build();

                entidad.AddPeriodoRepresentacion(periodoRepresentacion);
               
            }
        }
        await _context.Entidades.AddAsync(entidad);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Success(entidad.Id);
    }
}
