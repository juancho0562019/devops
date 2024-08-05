

using System.ComponentModel.DataAnnotations;
using Bext.Reps.Domain.Commons.DefaultMessages;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
public record TerceroPrestadorRequest(
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoPersona,
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string TipoIdentificacion,
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string NumeroIdentificacion,
    string? PrimerNombre,
    string? SegundoNombre,
    string? PrimerApellido,
    string? SegundoApellido,
    string? RazonSocial,
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Departamento,
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Municipio,
    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    string Direccion,
    string? TelefonoFijo,
    string? TelefonoMovil,
    string? TelefonoFax,
    string? SitioWeb,
    string? Email);
