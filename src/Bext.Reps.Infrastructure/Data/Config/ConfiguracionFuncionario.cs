using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionFuncionario : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable(@"Funcionarios");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Nombre).HasColumnName(@"Nombre").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.Property(x => x.EntidadId).HasColumnName(@"EntidadId").ValueGeneratedNever();
        builder.Property(x => x.CodigoInterno).HasColumnName(@"CodigoInterno").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.RolAplicacionId).HasColumnName(@"RolAplicacionId").ValueGeneratedNever();
        builder.Property(x => x.Email).HasColumnName(@"Email").IsRequired().ValueGeneratedNever().HasMaxLength(50);
        builder.ComplexProperty(x => x.RefreshToken, y =>
        {
            y.IsRequired();
            y.Property(z => z.Token).HasColumnName(@"Token").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            y.Property(z => z.FechaCreacion).HasColumnName(@"FechaCreacion").IsRequired().ValueGeneratedNever();
            y.Property(z => z.Expiracion).HasColumnName(@"Expiracion").IsRequired().ValueGeneratedNever();
        });
        builder.HasKey(@"Id");
        builder.HasOne(x => x.RolAplicacion)
            .WithMany(op => op.Funcionarios)
            .HasForeignKey(@"RolAplicacionId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}
