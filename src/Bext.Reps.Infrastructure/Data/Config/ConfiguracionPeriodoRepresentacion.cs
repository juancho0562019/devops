using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionPeriodoRepresentacion : IEntityTypeConfiguration<PeriodoRepresentacion>
{
    public void Configure(EntityTypeBuilder<PeriodoRepresentacion> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.TipoRepresentacion).IsRequired();
        builder.Property(v => v.FechaInicio).IsRequired();


        builder.HasOne(v => v.Entidad)
          .WithMany(v => v.Periodos)
          .HasForeignKey(v => v.EntidadId)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired();

        builder.HasOne(v => v.Contacto)
            .WithMany(v => v.PeriodosRepresentacion)
            .HasForeignKey(v => v.ContactoId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
