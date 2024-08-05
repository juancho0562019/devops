using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionCaracterTerritorial : IEntityTypeConfiguration<CaracterTerritorial>
{
    public void Configure(EntityTypeBuilder<CaracterTerritorial> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nombre).HasMaxLength(80).IsRequired();

        builder.HasOne(b => b.TipoNaturaleza)
            .WithMany(b => b.CaracterTerritorial)
            .HasForeignKey(b => b.TipoNaturalezaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
