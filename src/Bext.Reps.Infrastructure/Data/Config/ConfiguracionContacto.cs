using Bext.Reps.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Domain.Entities
{
    public class ConfiguracionContacto : IEntityTypeConfiguration<Contacto>
    {
        public void Configure(EntityTypeBuilder<Contacto> builder)
        {
            builder.ToTable(@"Contactos");
            builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TipoContacto).HasColumnName(@"TipoContacto").IsRequired().ValueGeneratedNever();
            builder.ComplexProperty(x => x.Nombre, y =>
            {
                y.IsRequired();
                y.Property(t => t.PrimerNombre).HasColumnName(@"PrimerNombre").IsRequired().ValueGeneratedNever().HasMaxLength(30);
                y.Property(t => t.PrimerApellido).HasColumnName(@"PrimeApellido").IsRequired().ValueGeneratedNever().HasMaxLength(30);
                y.Property(t => t.SegundoNombre).HasColumnName(@"SegundoNombre").ValueGeneratedNever().HasMaxLength(30);
                y.Property(t => t.SegundoApellido).HasColumnName(@"SegundoApellido").ValueGeneratedNever().HasMaxLength(30);
            });
            //builder.ComplexProperty(x => x.Identificacion, y =>
            //{
            //    y.IsRequired();
            //    y.Property(t => t.TipoIdentificacion).HasColumnName(@"TipoIdentificacion").IsRequired().ValueGeneratedNever();
            //    y.Property(t => t.NumeroDocumento).HasColumnName(@"NumeroDocumento").IsRequired().ValueGeneratedNever().HasMaxLength(30);
            //    y.Property(t => t.DigitoVerificacion).HasColumnName(@"DigitoVerificacion").IsRequired().ValueGeneratedNever();
            //});

            builder.OwnsOne(e => e.Identificacion, y =>
            {
                y.Property(i => i.TipoIdentificacionId).HasColumnName("TipoIdentificacionId").IsRequired();
                y.Property(i => i.NumeroDocumento).HasColumnName("NumeroDocumento").IsRequired().HasMaxLength(30);
                y.Property(i => i.DigitoVerificacion).HasColumnName("DigitoVerificacion").IsRequired();

                y.HasOne(i => i.TipoIdentificacion)
                 .WithMany()
                 .HasForeignKey(i => i.TipoIdentificacionId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Property(x => x.Telefono).HasColumnName(@"Telefono").IsRequired().ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.CorreoInstitucional).HasColumnName(@"CorreoInstitucional").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.TipoRepresentanteLegal).HasColumnName(@"TipoRepresentanteLegal").ValueGeneratedNever();
            builder.Property(x => x.TarjetaProfesional).HasColumnName(@"TarjetaProfesional").ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.Profesion).HasColumnName(@"Profesion").ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.InformacionOficio).HasColumnName(@"InformacionOficio").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.FechaDocumentoAutorizacion).HasColumnName(@"FechaDocumentoAutorizacion").ValueGeneratedNever();
            builder.Property(x => x.EntidadId).HasColumnName(@"EntidadId").ValueGeneratedNever();
            builder.HasKey(@"Id");
            builder.HasOne(x => x.Entidad).WithMany(op => op.Contactos).HasForeignKey(@"EntidadId").IsRequired(true);

        }
    }
}
