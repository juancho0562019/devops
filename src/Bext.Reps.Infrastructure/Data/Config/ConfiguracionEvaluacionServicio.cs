
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionEvaluacionServicio : IEntityTypeConfiguration<EvaluacionServicio>
{
    public void Configure(EntityTypeBuilder<EvaluacionServicio> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasOne(b => b.ServicioInscritoSede)
            .WithMany(b => b.Evaluaciones)
            .HasForeignKey(b => b.ServicioInscritoSedeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
