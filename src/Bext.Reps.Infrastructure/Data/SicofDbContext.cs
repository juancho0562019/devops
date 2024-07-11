using Bext.Reps.Business.Data;
using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.Primitives;
using Bext.Reps.Infrastructure.Data.Config;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bext.Reps.Infrastructure.Data;

public class RepsDbContext : DbContext, IRepsDbContext
{
    public RepsDbContext()
    {
    }
    // Create base DbContext class with DbSets from Bext.Reps.Domain.Entities
    public RepsDbContext(DbContextOptions<RepsDbContext> options) : base(options)
    {
    }

    public DbSet<Contacto> Contactos { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<DocumentoEntidad> DocumentosEntidad { get; set; }
    public DbSet<Entidad> Entidades { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Modalidad> Modalidades { get; set; }
    public DbSet<RegistroModalidad> RegistroModalidades { get; set; }
    public DbSet<RolAplicacion> RolesAplicacion { get; set; }
    public DbSet<Sede> Sedes { get; set; }
    public DbSet<Tercero> Terceros { get; set; }
    public DbSet<DocumentoIdentidad> DocumentosIdentidad { get; set; }
    public DbSet<TipoDocumento> TiposDocumentos { get; set; }

    public EntityEntry Entry<T, TKey>(T entity)
        where T : BaseEntity<TKey>
        where TKey : notnull
    {
        return base.Entry(entity);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        builder.ApplyConfiguration(new ConfiguracionContacto());
        builder.ApplyConfiguration(new ConfiguracionDocumento());
        builder.ApplyConfiguration(new ConfiguracionDocumentoEntidad());
        builder.ApplyConfiguration(new ConfiguracionEntidad());
        builder.ApplyConfiguration(new ConfiguracionFuncionario());
        builder.ApplyConfiguration(new ConfiguracionModalidad());
        builder.ApplyConfiguration(new ConfiguracionRegistroModalidad());
        builder.ApplyConfiguration(new ConfiguracionRolAplicacion());
        builder.ApplyConfiguration(new ConfiguracionSede());
        builder.ApplyConfiguration(new ConfiguracionTercero());


    }
}
