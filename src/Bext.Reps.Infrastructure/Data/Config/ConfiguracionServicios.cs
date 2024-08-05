using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionServicios : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nombre).HasMaxLength(180).IsRequired();

        builder.HasOne(b => b.GrupoServicio)
            .WithMany(b => b.Servicios)
            .HasForeignKey(b => b.GrupoServicioId)
            .IsRequired();
    }
}
