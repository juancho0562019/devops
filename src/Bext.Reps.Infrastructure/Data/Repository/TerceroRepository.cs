
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Infrastructure.Data.Repository;
public class TerceroRepository : ITerceroRepository
{
    private readonly IRepsDbContext _context;

    public TerceroRepository(IRepsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteTerceroAsync(string numeroIdentificacion)
    {
        return await _context.Terceros.AnyAsync(t => t.Identificacion.NumeroDocumento == numeroIdentificacion);
    }
}
