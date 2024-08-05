﻿using Bext.Reps.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bext.Reps.Infrastructure.Data.Config;

public class ConfiguracionDocumentoServicio : IEntityTypeConfiguration<DocumentoServicio>
{
    public void Configure(EntityTypeBuilder<DocumentoServicio> builder)
    {
        builder.ToTable(@"DocumentosServicios");
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Fecha).HasColumnName(@"Fecha").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Nombre).HasColumnName(@"Nombre").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Descripcion).HasColumnName(@"Descripcion").ValueGeneratedNever();
        builder.Property(x => x.SolicitudId).HasColumnName(@"SolicitudId").ValueGeneratedNever();
      
        builder.HasKey(@"Id");
        builder.HasOne(x => x.Solicitud).WithMany(op => op.Documentos).HasForeignKey(v => v.SolicitudId).IsRequired(true);
        builder.HasOne(x => x.TipoDocumento).WithMany().HasForeignKey(b => b.TipoDocumentoId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

    }
}