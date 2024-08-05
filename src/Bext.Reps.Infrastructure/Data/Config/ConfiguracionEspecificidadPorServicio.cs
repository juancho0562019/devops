
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionEspecificidadPorServicio : IEntityTypeConfiguration<EspecificidadPorServicioInscritoSede>
{
    public void Configure(EntityTypeBuilder<EspecificidadPorServicioInscritoSede> builder)
    {
        builder.HasKey(v => new { v.ServicioInscritoSedeId, v.EspecificidadId });
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.HasOne(b => b.Especificidad)
            .WithMany()
            .HasForeignKey(b => b.EspecificidadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.ServicioInscritoSede)
           .WithMany()
           .HasForeignKey(b => b.ServicioInscritoSedeId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
