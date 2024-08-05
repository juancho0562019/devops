using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;
using Bext.Reps.Infrastructure.Data.Config.Base;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionEntidad : IEntityTypeConfiguration<Entidad>
{
    public  void Configure(EntityTypeBuilder<Entidad> builder)
    {
 
        builder.ToTable(@"Entidades");
        builder.Property(x => x.Id)
            .HasColumnName(@"Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        //builder.Property(x => x.TipoNaturaleza)
        //    .HasColumnName(@"TipoNaturaleza")
        //    .HasMaxLength(2)
        //    .IsRequired();

        //builder.Property(x => x.SubTipoNaturaleza)
        //    .HasColumnName(@"SubTipoNaturaleza")
        //    .HasMaxLength(2)
        //    .IsRequired();     
        

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

        builder.ComplexProperty(x => x.DatosContacto, t =>
        {
            t.IsRequired();

            t.Property(x => x.Email)
                .HasColumnName(@"Email")
                .IsRequired()
                .HasMaxLength(250);

            t.Property(x => x.TelefonoFijo)
                .HasColumnName(@"TelefonoFijo")
                .IsRequired(true)
                .HasMaxLength(15);

            t.Property(x => x.TelefonoMovil)
                .HasColumnName(@"TelefonoMovil")
                .IsRequired(false)
                .HasMaxLength(15);

            t.Property(x => x.TelefonoFax)
                .HasColumnName(@"TelefonoFax")
                .HasMaxLength(15);

            t.Property(x => x.SitioWeb)
                .HasColumnName(@"SitioWeb")
                .HasMaxLength(250);
        });


        builder.HasOne(c => c.Tercero)
          .WithMany()
          .HasForeignKey(v => v.TerceroId)
          .IsRequired(true);

        builder.HasOne(c => c.ActaConstitucion)
           .WithMany()
           .HasForeignKey(v => v.ActaConstitucionId)
           .IsRequired(false);

        builder.HasMany(x => x.DocumentosEntidad)
            .WithOne(op => op.Entidad)
            .HasForeignKey(v => v.EntidadId)
            .IsRequired(true);

        builder.HasMany(x => x.Sedes)
            .WithOne(op => op.Entidad)
            .HasForeignKey(v => v.EntidadId)
            .IsRequired(true);

        builder.HasOne(x => x.TipoPersona)
            .WithMany()
            .HasForeignKey(x => x.TipoPersonaId)
            .IsRequired();

        builder.HasOne(x => x.TipoPrestador)
            .WithMany()
            .HasForeignKey(x => x.TipoPrestadorId)
            .IsRequired();

        builder.HasOne(x => x.TipoNaturaleza)
            .WithMany()
            .HasForeignKey(x => x.TipoNaturalezaId)
            .IsRequired(false);

    }
}
