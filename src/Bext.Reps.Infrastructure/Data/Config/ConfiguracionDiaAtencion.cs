using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bext.Reps.Infrastructure.Data.Config;
public class DiaAtencionConfiguration : IEntityTypeConfiguration<DiaAtencion>
{
    public void Configure(EntityTypeBuilder<DiaAtencion> builder)
    {

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DiaSemana)
               .IsRequired()
               .HasConversion<int>(); 
    }
}
