namespace Bext.Reps.Business.Commons.Interfaces.Repository;
public interface IEntidadRepository
{

    Task<Entidad?> GetByIdAsync(int id);
    Task<Entidad?> GetByIdWithSedesAsync(int id);
    Task<Sede?> GetSedePrincipalByIdAsync(int id);
    Task<bool> EsDocumentoRegistrado(int entidadId, int tipoDocumentoId);
}
