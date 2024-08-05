using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionSubTipoNaturaleza : IEntityTypeConfiguration<SubTipoNaturaleza>
{
    public void Configure(EntityTypeBuilder<SubTipoNaturaleza> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nombre).HasMaxLength(80).IsRequired();

        builder.HasOne(v => v.TipoNaturaleza)
            .WithMany(v => v.SubTipoNaturalezas)
            .HasForeignKey(v => v.TipoNaturalezaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
