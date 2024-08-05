
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionServicioInscritoSede : IEntityTypeConfiguration<ServicioInscritoSede>
{
    public void Configure(EntityTypeBuilder<ServicioInscritoSede> builder)
    {


        builder.HasKey(s => s.Id);

        builder.Property(s => s.SedeId).IsRequired();
        builder.Property(s => s.GrupoServicioId).IsRequired();
        builder.Property(s => s.ServicioId).IsRequired();
        builder.Property(s => s.SolicitudId).IsRequired();
        builder.Property(s => s.ComplejidadServicioId).IsRequired();

        builder.HasMany(s => s.FranjasHorarias)
               .WithOne(f => f.ServicioInscritoSede)
               .HasForeignKey(f => f.ServicioInscritoSedeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
