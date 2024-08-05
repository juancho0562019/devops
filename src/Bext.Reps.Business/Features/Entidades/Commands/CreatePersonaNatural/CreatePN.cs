using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Business.Features.Entidades.Commands.Create;
using Bext.Reps.Business.Models;
using Bext.Reps.Domain.Commons.DefaultMessages;
using MediatR;
using Bext.Reps.Business.Commons.Exceptions;
using Bext.Reps.Business.Commons.Interfaces;
using FluentValidation;

namespace Bext.Reps.Business.Features.Entidades.Commands.CreatePersonaNatural;
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
    string TipoPrestador,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoPersona,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoNaturaleza,

    string? SubTipoNaturaleza,
    
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    TerceroPrestadorRequest TerceroPrestadorRequest,

    List<SedeRequest>? SedeRequest
) : IRequest<Result<int>>;


public record CreateTerceroPN
(
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoPersona,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string NumeroIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string PrimerNombre,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string PrimerApellido,    

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Departamento,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Municipio,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Direccion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TelefonoMovil,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Correo,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    TerceroPrestadorRequest TerceroPrestadorRequest

) : IRequest<Result<int>>;

public record CrearTerceroPrestadorPJ
(
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string NumeroIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string RazonSocial,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Departamento,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Municipio,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Direccion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TelefonoMovil,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Correo

) : IRequest<Result<int>>;

public record CrearTerceroRepresentantePJ
(
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string NumeroIdentificacion,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string PrimerNombre,

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string PrimerApellido

) : IRequest<Result<int>>;

public class CreateTerceroPNHandler : IRequestHandler<CreateTerceroPN, Result<int>>
{
    private readonly IRepsDbContext _context;
    private readonly IValidator<CreateTerceroPN> _validator;
    public CreateTerceroPNHandler(IRepsDbContext context, IValidator<CreateTerceroPN> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateTerceroPN request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors);


        var identificacion = Identificacion.Crear(request.TerceroPrestadorRequest.TipoIdentificacion, request.TerceroPrestadorRequest.NumeroIdentificacion);

        var direccion = Ubicacion.Crear(request.TerceroPrestadorRequest.Departamento, request.TerceroPrestadorRequest.Municipio,
            request.TerceroPrestadorRequest.Direccion);

        var tercero = new Tercero.Builder(request.TerceroPrestadorRequest.TipoPersona,
                                             identificacion)
                .ConNombres(request.TerceroPrestadorRequest.RazonSocial, request.TerceroPrestadorRequest.PrimerNombre, request.TerceroPrestadorRequest.SegundoNombre, request.TerceroPrestadorRequest.PrimerApellido, request.TerceroPrestadorRequest.SegundoApellido)
                .ConUbicacion(direccion)
                .ConDatosContacto(request.TerceroPrestadorRequest.TelefonoFijo, request.TerceroPrestadorRequest.TelefonoMovil, request.TerceroPrestadorRequest.TelefonoFax, request.TerceroPrestadorRequest.SitioWeb, request.TerceroPrestadorRequest.Email)
                .Build();
        


        var identificacionEntidad = Identificacion.Crear(request.TipoIdentificacion, request.NumeroIdentificacion);

        var datosContacto = Contacto.Crear(null, request.TelefonoMovil, null, null, request.Correo);
        
        var entidad = new Entidad.Builder(0)
            .ConIdentificacion(identificacionEntidad)
            .ConDatosContacto(datosContacto)            
            .ConDireccion(request.Direccion)           
            .ConTipoPersona(request.TipoPersona)            
            .ConTercero(tercero)            
            .Build();


        await _context.Entidades.AddAsync(entidad);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Success(entidad.Id);
    }
}
