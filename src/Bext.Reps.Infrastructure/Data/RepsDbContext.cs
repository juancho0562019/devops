using System.Reflection;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Domain.Commons.Primitives;
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bext.Reps.Infrastructure.Data;

public class RepsDbContext : DbContext, IRepsDbContext
{
    public RepsDbContext()
    {
    }
   
    public RepsDbContext(DbContextOptions<RepsDbContext> options) : base(options)
    {
    }

    public DbSet<DocumentoEntidad> DocumentosEntidad { get; set; }
    public DbSet<Entidad> Entidades { get; set; }
    public DbSet<Solicitud> Solicitudes { get; set; }
    public DbSet<Sede> Sedes { get; set; }
    public DbSet<Tercero> Terceros { get; set; }
    public DbSet<TipoPersona> TiposPersona { get; set; }
    public DbSet<TipoIdentidad> TiposIdentidad { get; set; }
    public DbSet<ClasePrestador> ClasesPrestador{ get; set; }
    public DbSet<TipoNaturaleza> TiposNaturaleza{ get; set; }
    public DbSet<SubTipoNaturaleza> SubTiposNaturaleza{ get; set; }
    public DbSet<TipoDocumento> TiposDocumentos { get; set; }
    public DbSet<NivelAtencion> NivelesAtencion { get; set; }
    public DbSet<DocumentoConstitucion> DocumentosConstitucion { get; set; }
    public DbSet<CaracterTerritorial> CaracteresTerritoriales { get; set; }
    public DbSet<TipoVinculacion> TiposVinculacion { get; set; }
    public DbSet<GrupoServicio> GruposServicio { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Estandar> Estandares { get; set; }
    public DbSet<Criterio> Criterios { get; set; }
    public DbSet<EstandarPorServicio> EstandarPorServicios { get; set; }
    public DbSet<EspecificidadPorServicioInscritoSede> EspecificidadPorServiciosInscritosSede { get; set; }
    public DbSet<Especificidad> Especificidades { get; set; }
    public DbSet<Complejidad> Complejidades { get; set; }
    public DbSet<DiaAtencion> DiasAtencion { get; set; }
    public DbSet<FranjaHoraria> FranjasHoraria { get; set; }
    public EntityEntry Entry<T, TKey>(T entity)
        where T : BaseEntity<TKey>
        where TKey : notnull
    {
        return base.Entry(entity);
    }
    public DbSet<TEntity> Set<TEntity, TKey>() where TEntity : BaseEntity<TKey> where TKey : notnull
    {
        return base.Set<TEntity>();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
