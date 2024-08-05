using System.ComponentModel.DataAnnotations;
using Bext.Reps.Domain.Commons.DefaultMessages;

namespace Bext.Reps.Business.Features.Entidades.Commands.Commons.Request;
public abstract class BaseSedeRequest
{
    public bool EsPrincipal { get; set; }

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string NombreSede { get; set; } = string.Empty;

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string NombreResponsable { get; set; } = string.Empty;

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Departamento { get; set; } = string.Empty;

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Municipio { get; set; } = string.Empty;

    public string? CentroPoblado { get; set; }

    public string Zona { get; set; } = string.Empty;

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = DefaultMessage.IsRequired)]
    public string Barrio { get; set; } = string.Empty;

    public string? TelefonoFijo { get; set; }

    public string? TelefonoMovil { get; set; }

    public string? TelefonoFax { get; set; }

    public string? Email { get; set; }

}
