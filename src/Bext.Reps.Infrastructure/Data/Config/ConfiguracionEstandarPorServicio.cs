
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionEstandarPorServicio : IEntityTypeConfiguration<EstandarPorServicio>
{
    public void Configure(EntityTypeBuilder<EstandarPorServicio> builder)
    {
        builder.HasKey(v => new { v.ServicioId, v.EstandarId });
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.HasOne(b => b.Estandar)
            .WithMany()
            .HasForeignKey(b => b.EstandarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Servicio)
           .WithMany(v => v.Estandares)
           .HasForeignKey(b => b.ServicioId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
