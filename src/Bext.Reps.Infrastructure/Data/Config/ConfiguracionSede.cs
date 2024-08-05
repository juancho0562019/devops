using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionSede : IEntityTypeConfiguration<Sede>
{
    public void Configure(EntityTypeBuilder<Sede> builder)
    {
        builder.ToTable(@"Sedes");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Nombre).HasColumnName(@"Nombre").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        
        builder.ComplexProperty(x => x.Ubicacion, t =>
        {
            t.IsRequired();
            t.Property(x => x.Departamento).HasColumnName(@"Departamento").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            t.Property(x => x.Municipio).HasColumnName(@"Municipio").IsRequired().ValueGeneratedNever().HasMaxLength(50);
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

        builder.Property(x => x.EntidadId).HasColumnName(@"EntidadId").ValueGeneratedNever();
        builder.HasKey(@"Id");
        
        builder.HasOne(x => x.Entidad)
            .WithMany(op => op.Sedes)
            .HasForeignKey(@"EntidadId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);

    }
}
