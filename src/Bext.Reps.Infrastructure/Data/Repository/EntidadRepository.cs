using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Infrastructure.Data.Repository;
public class EntidadRepository : IEntidadRepository
{
    private readonly IRepsDbContext _context;

    public EntidadRepository(IRepsDbContext context) 
    {
        _context = context;
    }
    public async Task<Entidad?> GetByIdAsync(int id)
    {
        return await _context.Entidades.Include(v => v.TipoPersona).Include(v => v.TipoNaturaleza).Include(v => v.Tercero).Where(v => v.Id.Equals(id)).FirstOrDefaultAsync();
    }
    public async Task<bool> EsDocumentoRegistrado(int entidadId, int tipoDocumentoId)
    {
        return await _context.Entidades.Include(b => b.DocumentosEntidad).Where(b => b.DocumentosEntidad.Select(c => c.TipoDocumentoId).Contains(tipoDocumentoId) && b.Id == entidadId).AnyAsync();
    }
    public async Task<Entidad?> GetByIdWithSedesAsync(int id)
    {
        return await _context.Entidades.Include(v => v.TipoPersona).Include(v => v.TipoNaturaleza).Include(v => v.Tercero).Include(v => v.Sedes).Where(v => v.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<Sede?> GetSedePrincipalByIdAsync(int id) 
    {
        return await _context.Sedes.Where(v => v.EntidadId.Equals(id)).FirstOrDefaultAsync();
    }
} 
