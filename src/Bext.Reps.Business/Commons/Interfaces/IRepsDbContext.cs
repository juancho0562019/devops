using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Bext.Reps.Domain.Commons.Primitives;

namespace Bext.Reps.Business.Commons.Interfaces;

public interface IRepsDbContext
{

    public DbSet<DocumentoEntidad> DocumentosEntidad { get; }
    public DbSet<Entidad> Entidades { get; }
    public DbSet<Solicitud> Solicitudes { get; }
    public DbSet<TipoPersona> TiposPersona { get; }
    public DbSet<ClasePrestador> ClasesPrestador { get; }
    public DbSet<TipoIdentidad> TiposIdentidad { get; }
    public DbSet<TipoNaturaleza> TiposNaturaleza { get; }
    public DbSet<SubTipoNaturaleza> SubTiposNaturaleza { get; }
    public DbSet<TipoDocumento> TiposDocumentos { get; }
    public DbSet<NivelAtencion> NivelesAtencion { get; }
    public DbSet<Sede> Sedes { get; }
    public DbSet<Tercero> Terceros { get; }
    public DbSet<TipoVinculacion> TiposVinculacion { get; }
    public DbSet<DocumentoConstitucion> DocumentosConstitucion { get; }
    public DbSet<CaracterTerritorial> CaracteresTerritoriales { get; }
    public DbSet<GrupoServicio> GruposServicio { get; }
    public DbSet<Servicio> Servicios { get; }
    public DbSet<Estandar> Estandares { get;}
    public DbSet<Criterio> Criterios { get;}
    public DbSet<EstandarPorServicio> EstandarPorServicios { get; }
    public DbSet<Especificidad> Especificidades { get; }
    public DbSet<DiaAtencion> DiasAtencion { get;  }
    public DbSet<FranjaHoraria> FranjasHoraria { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry Entry<T, TKey>(T entity) where T : BaseEntity<TKey> where TKey : notnull;

    DbSet<TEntity> Set<TEntity, TKey>() where TEntity : BaseEntity<TKey> where TKey : notnull;
}
