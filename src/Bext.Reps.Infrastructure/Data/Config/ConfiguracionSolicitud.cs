using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionSolicitud : IEntityTypeConfiguration<Solicitud>
{
    public void Configure(EntityTypeBuilder<Solicitud> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasOne(b => b.Entidad)
            .WithMany()
            .HasForeignKey(b => b.EntidadId)
            .IsRequired();
    }
}
