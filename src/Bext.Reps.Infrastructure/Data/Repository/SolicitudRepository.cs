using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Bext.Reps.Infrastructure.Data.Repository;
public class SolicitudRepository : ISolicitudRepository
{
    private readonly IRepsDbContext _context;

    public SolicitudRepository(IRepsDbContext context) 
    {
        _context = context;
    }

    public async Task<bool> EsDocumentoRegistrado(int solicitudId, int tipoDocumentoId)
    {
        return await _context.Solicitudes.Include(b => b.Documentos).Where(b => b.Documentos.Select(c => c.TipoDocumentoId).Contains(tipoDocumentoId) && b.Id == solicitudId).AnyAsync();
    }

} 
