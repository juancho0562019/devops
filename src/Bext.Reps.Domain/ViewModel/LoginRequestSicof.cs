using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bext.Reps.Domain.ViewModel
{
    public class LoginRequestReps
    {
        public required string Usuario { get; init; }
        public required string Clave { get; init; }
        public required bool EsInterno { get; init; } = false;
    }
}
