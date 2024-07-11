using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.ValueObjects;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionTercero : IEntityTypeConfiguration<Tercero>
{
    public void Configure(EntityTypeBuilder<Tercero> builder)
    {
        builder.ToTable(@"Terceros");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
     
        builder.OwnsOne(p => p.Identificacion, y =>
        {
            y.Property(i => i.TipoIdentificacionId).HasColumnName("TipoIdentificacionId").IsRequired();
            y.Property(i => i.NumeroDocumento).HasColumnName("NumeroDocumento").IsRequired().HasMaxLength(30);
            y.Property(i => i.DigitoVerificacion).HasColumnName("DigitoVerificacion").IsRequired();

            y.HasOne(i => i.TipoIdentificacion)
             .WithMany()
             .HasForeignKey(i => i.TipoIdentificacionId);
        });

        builder.ComplexProperty(x => x.Ubicacion, y =>
        {
            y.IsRequired();
            y.Property(t => t.Pais).HasColumnName(@"Pais").IsRequired().HasMaxLength(2);
            y.Property(t => t.Departamento).HasColumnName(@"Departamento").IsRequired().HasMaxLength(2);
            y.Property(t => t.Municipio).HasColumnName(@"Municipio").IsRequired().HasMaxLength(3);
            y.Property(t => t.Direccion).HasColumnName(@"Direccion").IsRequired().HasMaxLength(80);
        });

        builder.Property(x => x.PrimerNombre)
            .HasColumnName(@"PrimerNombre")
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.SegundoNombre)
            .HasColumnName(@"SegundoNombre")
            .HasMaxLength(20);
        
        builder.Property(x => x.PrimerApellido)
            .HasColumnName("PrimerApellido")
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.SegundoApellido)
            .HasColumnName("SegundoApellido")
            .HasMaxLength(20);

        builder.Property(x => x.RazonSocial)
         .HasColumnName("RazonSocial")
         .HasMaxLength(250);


        builder.Property(x => x.TelefonoMovil)
            .HasColumnName("TelefonoMovil")
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.TelefonoFijo)
            .HasColumnName("TelefonoFijo")
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.TelefonoFax)
            .HasColumnName("TelefonoFax")
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Email)
            .HasColumnName(@"Email")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SitioWeb)
            .HasColumnName(@"SitioWeb")
            .IsRequired()
            .HasMaxLength(100);

        builder.HasKey(@"Id");
    

    }
}
