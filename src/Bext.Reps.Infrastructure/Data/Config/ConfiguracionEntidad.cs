using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;
using Bext.Reps.Domain.ValueObjects;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionEntidad : IEntityTypeConfiguration<Entidad>
{
    public void Configure(EntityTypeBuilder<Entidad> builder)
    {
        builder.ToTable(@"Entidades");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.TipoNaturalezaJuridica).HasColumnName(@"TipoNaturalezaJuridica").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.TipoEntidad).HasColumnName(@"TipoEntidad").IsRequired().ValueGeneratedNever();
        //builder.ComplexProperty(x => x.Identificacion, y =>
        //{
        //    y.IsRequired();
        //    y.Property(z => z.TipoIdentificacion).HasColumnName(@"TipoIdentificacion").IsRequired().ValueGeneratedNever();
        //    y.Property(z => z.NumeroDocumento).HasColumnName(@"NumeroDocumento").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        //    y.Property(z => z.DigitoVerificacion).HasColumnName(@"DigitoVerificacion").IsRequired().ValueGeneratedNever();
        //});

        builder.OwnsOne(e => e.Identificacion, y =>
        {
            y.Property(i => i.TipoIdentificacionId).HasColumnName("TipoIdentificacionId").IsRequired();
            y.Property(i => i.NumeroDocumento).HasColumnName("NumeroDocumento").IsRequired().HasMaxLength(30);
            y.Property(i => i.DigitoVerificacion).HasColumnName("DigitoVerificacion").IsRequired();

            y.HasOne(i => i.TipoIdentificacion)
             .WithMany()
             .HasForeignKey(i => i.TipoIdentificacionId)
             .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Property(x => x.Nombre).HasColumnName(@"Nombre").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.Sigla).HasColumnName(@"Sigla").ValueGeneratedNever().HasMaxLength(50);
        builder.ComplexProperty(x => x.Ubicacion, t =>
        {
            t.IsRequired();
            t.Property(z => z.Pais).HasColumnName(@"Pais").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            t.Property(z => z.Departamento).HasColumnName(@"Departamento").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            t.Property(z => z.Municipio).HasColumnName(@"Municipio").IsRequired().HasMaxLength(50);
        });
       
        builder.Property(x => x.CorreoElectronico).HasColumnName(@"CorreoElectronico").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.TelefonoPrincipal).HasColumnName(@"TelefonoPrincipal").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.TelefonoAdicional).HasColumnName(@"TelefonoAdicional").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        
        builder.HasKey(@"Id");
        builder.HasMany(x => x.Contactos)
            .WithOne(op => op.Entidad)
            .HasForeignKey(@"EntidadId")
            .IsRequired(true);


        builder.HasMany(x => x.RegistrosModalidad)
            .WithOne(op => op.Entidad)
            .HasForeignKey(@"EntidadId")
            .IsRequired(true);
        
        builder.HasMany(x => x.DocumentosEntidad)
            .WithOne(op => op.Entidad)
            .HasForeignKey(@"EntidadId")
            .IsRequired(true);

        builder.HasMany(x => x.Sedes)
            .WithOne(op => op.Entidad)
            .HasForeignKey(@"EntidadId")
            .IsRequired(true);

        //builder.HasIndex(b => b.Identificacion => ( c => c.NumeroDocumento))
        //    .HasDatabaseName("IX_NumeroDocumento_Ascending");
    }
}
