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
        builder.Property(x => x.MatriculaMercantil).HasColumnName(@"MatriculaMercantil").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.CodigoSede).HasColumnName(@"CodigoSede").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.ComplexProperty(x => x.Ubicacion, t =>
        {
            t.IsRequired();
            t.Property(x => x.Pais).HasColumnName(@"Pais").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            t.Property(x => x.Departamento).HasColumnName(@"Departamento").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            t.Property(x => x.Municipio).HasColumnName(@"Municipio").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        });

        builder.Property(x => x.DireccionNotificacionJudicial).HasColumnName(@"DireccionNotificacionJudicial").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.CorreoElectronico).HasColumnName(@"CorreoElectronico").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.Telefono).HasColumnName(@"Telefono").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.RegistroMercantil).HasColumnName(@"RegistroMercantil").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.EntidadId).HasColumnName(@"EntidadId").ValueGeneratedNever();
        builder.HasKey(@"Id");
        
        builder.HasOne(x => x.Entidad)
            .WithMany(op => op.Sedes)
            .HasForeignKey(@"EntidadId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);

    }
}
