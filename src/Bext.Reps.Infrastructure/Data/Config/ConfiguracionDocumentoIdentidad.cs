using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionDocumentoIdentidad : IEntityTypeConfiguration<DocumentoIdentidad>
{
    public void Configure(EntityTypeBuilder<DocumentoIdentidad> builder)
    {
        builder.ToTable(@"DocumentosIdentidad");

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
