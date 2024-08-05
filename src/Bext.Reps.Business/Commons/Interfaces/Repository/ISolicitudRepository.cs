namespace Bext.Reps.Business.Commons.Interfaces.Repository;
public interface ISolicitudRepository
{
    Task<bool> EsDocumentoRegistrado(int solicitudId, int tipoDocumentoId);
}
