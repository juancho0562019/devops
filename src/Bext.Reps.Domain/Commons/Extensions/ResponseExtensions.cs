using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bext.Reps.Domain.Commons.Extensions
{
    public static class ResponseExtensions
    {
        public static RolResponse ToRolResponse(this RolAplicacion rol)
        {
            return new RolResponse
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                EsInterno = rol.EsInterno
            };
        }
    }
}
