
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionCapacidadInstalada : IEntityTypeConfiguration<CapacidadInstalada>
{
    public void Configure(EntityTypeBuilder<CapacidadInstalada> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(ci => ci.TipoRecurso)
              .IsRequired()
              .HasConversion<string>();

        builder.HasOne(v => v.ServicioInscritoSede)
              .WithMany(sis => sis.CapacidadesInstaladas)
              .HasForeignKey(ci => ci.ServicioInscritoSedeId)
              .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(ci => new { ci.ServicioInscritoSedeId, ci.TipoRecurso, ci.Activo })
              .HasDatabaseName("IX_CapacidadesInstaladas_ServicioInscritoSede_TipoRecurso_Activo");
    }
}
