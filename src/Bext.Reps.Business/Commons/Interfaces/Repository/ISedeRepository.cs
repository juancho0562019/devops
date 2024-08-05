namespace Bext.Reps.Business.Commons.Interfaces.Repository;
public interface ISedeRepository
{
    Task<bool> EsDocumentoRegistrado(int sedeId, int tipoDocumentoId);
    Task<Sede?> GetByIdAsync(int id);
}
