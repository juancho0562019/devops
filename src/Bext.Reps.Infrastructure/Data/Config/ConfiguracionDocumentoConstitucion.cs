using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionDocumentoConstitucion : IEntityTypeConfiguration<DocumentoConstitucion>
{
    public void Configure(EntityTypeBuilder<DocumentoConstitucion> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nombre).HasMaxLength(80).IsRequired();

        builder.HasOne(b => b.SubTipoNaturaleza)
            .WithMany(c => c.DocumentosConstitucion)
            .HasForeignKey(b => b.SubTipoNaturalezaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
