using System.ComponentModel.DataAnnotations;

namespace Bext.Reps.Domain.Commons.Enums;
public enum EstadoDocumento
{
    [Display(Name = "Recibido")]
    Recibido,
    [Display(Name = "Radicado")]
    Radicado
}
