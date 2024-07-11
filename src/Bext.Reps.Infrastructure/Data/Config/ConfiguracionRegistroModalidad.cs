using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionRegistroModalidad : IEntityTypeConfiguration<RegistroModalidad>
{
    public void Configure(EntityTypeBuilder<RegistroModalidad> builder)
    {
        builder.ToTable(@"RegistroModalidades");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ModalidadId).HasColumnName(@"ModalidadId").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Fecha).HasColumnName(@"Fecha").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Estado).HasColumnName(@"EstadoRegistro").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.FuncionarioInternoId).HasColumnName(@"FuncionarioInternoId").ValueGeneratedNever();
        builder.Property(x => x.FuncionarioExternoId).HasColumnName(@"FuncionarioExternoId").ValueGeneratedNever();
        builder.Property(x => x.EntidadId).HasColumnName(@"EntidadId").ValueGeneratedNever();
        builder.HasKey(@"Id");
        builder.HasOne(x => x.FuncionarioInterno).WithMany().HasForeignKey(@"FuncionarioInternoId").OnDelete(DeleteBehavior.NoAction).IsRequired(true);
        builder.HasOne(x => x.FuncionarioExterno).WithMany().HasForeignKey(@"FuncionarioExternoId").OnDelete(DeleteBehavior.NoAction).IsRequired(true);
        builder.HasOne(x => x.Entidad).WithMany(op => op.RegistrosModalidad).HasForeignKey(@"EntidadId").IsRequired(true);
        builder.HasOne(x => x.Modalidad).WithMany(op => op.RegistrosModalidad).HasForeignKey(@"ModalidadId").IsRequired(true);
        builder.HasMany(x => x.Documentos).WithOne(op => op.RegistroModalidad).HasForeignKey(@"RegistroModalidadId").IsRequired(true);
    }
}
