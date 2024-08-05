using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionTipoNaturaleza : IEntityTypeConfiguration<TipoNaturaleza>
{
    public void Configure(EntityTypeBuilder<TipoNaturaleza> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).HasMaxLength(2).IsRequired();
        builder.Property(v => v.Nombre).HasMaxLength(80).IsRequired();


    }
}
