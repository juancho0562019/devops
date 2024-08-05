using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Infrastructure.Data.Config;
public class FranjaHorariaConfiguration : IEntityTypeConfiguration<FranjaHoraria>
{
    public void Configure(EntityTypeBuilder<FranjaHoraria> builder)
    {

        builder.HasKey(f => f.Id);

        builder.Property(f => f.HoraApertura).IsRequired();
        builder.Property(f => f.HoraCierre).IsRequired();

        builder.HasMany(f => f.DiasAtencion)
               .WithOne(d => d.FranjaHoraria)
               .HasForeignKey(d => d.FranjaHorariaId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
