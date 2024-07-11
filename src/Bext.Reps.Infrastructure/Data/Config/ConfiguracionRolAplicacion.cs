using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionRolAplicacion : IEntityTypeConfiguration<RolAplicacion>
{
    public void Configure(EntityTypeBuilder<RolAplicacion> builder)
    {
        builder.ToTable(@"RolesAplicacion");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Nombre).HasColumnName(@"Nombre").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.EsInterno).HasColumnName(@"EsInterno").IsRequired().ValueGeneratedNever();
        builder.HasKey(@"Id");
        builder.HasMany(x => x.Funcionarios).WithOne(op => op.RolAplicacion).HasForeignKey(@"RolAplicacionId").IsRequired(true);

    }
}
