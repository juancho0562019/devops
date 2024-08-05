
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionCriterio : IEntityTypeConfiguration<Criterio>
{
    public void Configure(EntityTypeBuilder<Criterio> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nombre).HasMaxLength(250).IsRequired();

        builder.HasOne(b => b.Estandar)
            .WithMany(b => b.Criterios)
            .HasForeignKey(b => b.EstandarId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
