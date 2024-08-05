using Bext.Reps.Domain.Commons.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config.Base;
public class BasePersonaConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : Persona<TId> where TId: notnull
{
   
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
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
                .IsRequired()
                .HasMaxLength(15);

            t.Property(x => x.TelefonoMovil)
                .HasColumnName(@"TelefonoMovil")
                .IsRequired()
                .HasMaxLength(15);

            t.Property(x => x.TelefonoFax)
                .HasColumnName(@"TelefonoMovil")
                .IsRequired()
                .HasMaxLength(15);

            t.Property(x => x.SitioWeb)
                .HasColumnName(@"SitioWeb")
                .HasMaxLength(250);
        });
    }
}
