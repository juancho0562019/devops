using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;
using Bext.Reps.Infrastructure.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionTercero : IEntityTypeConfiguration<Tercero>
{
    public  void Configure(EntityTypeBuilder<Tercero> builder)
    {
        

        builder.ToTable("Terceros");
        builder.Property(x => x.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
        //builder.Property(x => x.TipoPersona).HasColumnName("TipoPersona").IsRequired().HasMaxLength(2);

        builder.HasKey(x => x.Id);

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

        builder.ComplexProperty(x => x.Ubicacion, y =>
        {
            y.IsRequired();
            y.Property(t => t.Departamento).HasColumnName(@"Departamento").IsRequired().HasMaxLength(2);
            y.Property(t => t.Municipio).HasColumnName(@"Municipio").IsRequired().HasMaxLength(5);
            y.Property(t => t.Direccion).HasColumnName(@"Direccion").IsRequired().HasMaxLength(250);
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

        builder.ComplexProperty(x => x.DatosContacto, t =>
        {
            t.IsRequired();

            t.Property(x => x.Email)
                .HasColumnName(@"Email")
                .IsRequired()
                .HasMaxLength(250);

            t.Property(x => x.TelefonoFijo)
                .HasColumnName(@"TelefonoFijo")
                .HasMaxLength(15);

            t.Property(x => x.TelefonoMovil)
                .HasColumnName(@"TelefonoMovil")
                .IsRequired()
                .HasMaxLength(15);

            t.Property(x => x.TelefonoFax)
                .HasColumnName(@"TelefonoFax")
                .HasMaxLength(15);

            t.Property(x => x.SitioWeb)
                .HasColumnName(@"SitioWeb")
                .HasMaxLength(250);
        });


        builder.HasOne(x => x.TipoPersona)
            .WithMany()
            .HasForeignKey(x => x.TipoPersonaId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            
    }
}
