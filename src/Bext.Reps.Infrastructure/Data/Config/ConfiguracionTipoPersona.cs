using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionTipoPersona : IEntityTypeConfiguration<TipoPersona>
{
    public void Configure(EntityTypeBuilder<TipoPersona> builder)
    {
        builder.ToTable(@"TiposPersona");

        builder.Property(x => x.Id)
            .HasColumnName(@"Id")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasColumnName(@"Nombre")
            .IsRequired()
            .HasMaxLength(50);

       
    }
}
