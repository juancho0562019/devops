using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionContactoEntidad : IEntityTypeConfiguration<ContactoEntidad>
{
    public void Configure(EntityTypeBuilder<ContactoEntidad> builder)
    {
        builder.ToTable(@"ContactosEntidad");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();

        builder.ComplexProperty(p => p.Identificacion, y =>
        {
            y.Property(i => i.TipoIdentificacion)
                .HasColumnName("TipoIdentificacion")
                .HasMaxLength(2)
                .IsRequired();
            y.Property(i => i.NumeroDocumento)
                .HasColumnName("NumeroDocumento")
                .IsRequired()
                .HasMaxLength(30);
            y.Property(i => i.DigitoVerificacion).HasColumnName("DigitoVerificacion").IsRequired();
        });

        builder.ComplexProperty(x => x.Nombre, y =>
        {
            y.IsRequired();
            y.Property(t => t.PrimerNombre).HasColumnName(@"PrimerNombre")
                .HasMaxLength(20);
            y.Property(t => t.SegundoNombre).HasColumnName(@"SegundoNombre")
                .HasMaxLength(20);
            y.Property(t => t.PrimerApellido)
                .HasColumnName(@"PrimerApellido")
                .HasMaxLength(20);
            y.Property(t => t.SegundoApellido)
                .HasColumnName(@"SegundoApellido")
                .HasMaxLength(20);

            y.Property(t => t.RazonSocial)
                .HasColumnName(@"RazonSocial")
                .HasMaxLength(250);
        });

        builder.HasOne(b => b.TipoVinculacion)
            .WithMany()
            .HasForeignKey(b => b.TipoVinculacionId)
            .IsRequired(false);
    }
}
