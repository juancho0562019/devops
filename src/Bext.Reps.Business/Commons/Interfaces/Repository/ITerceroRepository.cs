using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bext.Reps.Business.Commons.Interfaces.Repository;
public interface ITerceroRepository
{
    Task<bool> ExisteTerceroAsync(string numeroIdentificacion);
}
