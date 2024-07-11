using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bext.Reps.Domain.ViewModel
{
    public class LoginResponseReps
    {
        public required Guid Id { get; init; }
        public required string Usuario { get; init; }
        public required string Nombre { get; init; }
        public required string Correo { get; init; }
        public required bool EsInterno { get; init; } = false;
        public required string Rol { get; init; }
    }
}
