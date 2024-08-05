using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionActaConstitucion : IEntityTypeConfiguration<ActaConstitucion>
{
    public void Configure(EntityTypeBuilder<ActaConstitucion> builder)
    {
        builder.ToTable(@"ActasConstitucion");
        
        builder.Property(x => x.Id)
            .HasColumnName(@"Id")
            .IsRequired();
        

       

        builder.Property(x => x.EmpresaSocialEstado)
                .HasColumnName(@"EmpresaSocialEstado")
                .IsRequired(false);

        builder.Property(x => x.NumeroActo).HasColumnName(@"NumeroActo").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.FechaActo).HasColumnName(@"FechaActo").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.EntidadExpide).HasColumnName(@"EntidadExpide").ValueGeneratedNever();
        builder.Property(x => x.CiudadExpedicion).HasColumnName(@"CiudadExpedicion").ValueGeneratedNever();
      



        builder.HasOne(x => x.ActoConstitucion).WithMany().HasForeignKey(b => b.ActoConstitucionId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CaracterTerritorial).WithMany().HasForeignKey(b => b.CaracterTerritorialId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.NivelAtencion).WithMany().HasForeignKey(b => b.NivelAtencionId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);


    }
}
