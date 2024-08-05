using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;
public class ConfiguracionGrupoServicio : IEntityTypeConfiguration<GrupoServicio>
{
    public void Configure(EntityTypeBuilder<GrupoServicio> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nombre).HasMaxLength(180).IsRequired();
        builder.HasOne(v => v.Modalidad).WithMany().HasForeignKey(v => v.ModalidadId);
       
    }
}
