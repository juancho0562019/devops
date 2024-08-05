
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionDetalleEvaluacion : IEntityTypeConfiguration<DetalleEvaluacionServicio>
{
    public void Configure(EntityTypeBuilder<DetalleEvaluacionServicio> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasOne(b => b.Estandar)
            .WithMany()
            .HasForeignKey(b => b.EstandarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Criterio)
            .WithMany()
            .HasForeignKey(b => b.CriterioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
