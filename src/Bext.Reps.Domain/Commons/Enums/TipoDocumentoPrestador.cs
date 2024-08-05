using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bext.Reps.Domain.Commons.Enums;
public enum TipoDocumentoPrestador
{
    [Display(Name = "Prestador")]
    Prestador,
    [Display(Name = "Sede")]
    Sede,
    [Display(Name = "Servicio")]
    Servicio
}
