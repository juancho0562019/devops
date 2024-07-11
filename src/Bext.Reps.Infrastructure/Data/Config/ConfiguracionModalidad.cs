using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionModalidad : IEntityTypeConfiguration<Modalidad>
{
    public void Configure(EntityTypeBuilder<Modalidad> builder)
    {
        builder.ToTable(@"Modalidades");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Descripcion).HasColumnName(@"Descripcion").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.HasKey(@"Id");

        builder.Property(m => m.Descripcion)
            .IsRequired()
            .HasMaxLength(255);
    }
}
