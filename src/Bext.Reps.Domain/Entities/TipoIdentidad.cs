using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Domain.Entities;
public class TipoIdentidad: BaseEntity<string>
{
    public string Nombre { get; set; } = string.Empty;
}
