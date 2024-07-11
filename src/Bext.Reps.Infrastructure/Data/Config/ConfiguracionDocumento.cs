using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionDocumento : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.ToTable(@"Documentos");
        builder.Property(x => x.Id)
            .HasColumnName(@"Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Fecha)
            .HasColumnName(@"Fecha")
            .IsRequired();

        builder.Property(x => x.Link)
            .HasColumnName(@"Link")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Nombre)
            .HasColumnName(@"Nombre")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Descripcion)
            .HasColumnName(@"Descripcion")
            .HasMaxLength(50);

        builder.Property(x => x.RegistroModalidadId).HasColumnName(@"RegistroModalidadId").ValueGeneratedNever();
        builder.HasKey(@"Id");
        builder.HasOne(x => x.RegistroModalidad).WithMany(op => op.Documentos).HasForeignKey(@"RegistroModalidadId").IsRequired(true);

        builder.Property(x => x.Descripcion)
            .IsRequired()
            .HasMaxLength(200);
    }
}
