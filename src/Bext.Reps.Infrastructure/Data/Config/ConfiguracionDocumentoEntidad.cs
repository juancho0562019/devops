using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionDocumentoEntidad : IEntityTypeConfiguration<DocumentoEntidad>
{
    public void Configure(EntityTypeBuilder<DocumentoEntidad> builder)
    {
        builder.ToTable(@"DocumentosEntidad");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Fecha).HasColumnName(@"Fecha").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Link).HasColumnName(@"Link").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.Nombre).HasColumnName(@"Nombre").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.Descripcion).HasColumnName(@"Descripcion").ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.EntidadId).HasColumnName(@"EntidadId").ValueGeneratedNever();
      
        builder.HasKey(@"Id");
        builder.HasOne(x => x.Entidad).WithMany(op => op.DocumentosEntidad).HasForeignKey(@"EntidadId").IsRequired(true);
        builder.HasOne(x => x.TipoDocumento).WithMany().HasForeignKey(b => b.TipoDocumentoId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

    }
}
