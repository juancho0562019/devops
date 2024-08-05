using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Bext.Reps.Infrastructure.Data.Repository;
public class SedeRepository : ISedeRepository
{
    private readonly IRepsDbContext _context;

    public SedeRepository(IRepsDbContext context) 
    {
        _context = context;
    }

    public async Task<bool> EsDocumentoRegistrado(int sedeId, int tipoDocumentoId)
    {
        return await _context.Sedes.Include(b => b.Documentos).Where(b => b.Documentos.Select(c => c.TipoDocumentoId).Contains(tipoDocumentoId) && b.Id == sedeId).AnyAsync();
    }

    public async Task<Sede?> GetByIdAsync(int id)
    {
        return await _context.Sedes.Where(v => v.Id == id).FirstOrDefaultAsync();
    }
} 
