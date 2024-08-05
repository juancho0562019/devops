using System.ComponentModel.DataAnnotations;

namespace Bext.Reps.Domain.Commons.Enums;
public enum TipoRecurso
{
    [Display(Name = "Cama")]
    Cama,
    [Display(Name = "Sala")]
    Sala,
    [Display(Name = "Consultorio")]
    Consultorio
}
