using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class CatPeriodoConfiguration
      : IEntityTypeConfiguration<CatPeriodo>
    {
        public void Configure(
            EntityTypeBuilder<CatPeriodo> builder)
        {
            builder.ToTable("CatPeriodo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StrValor)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.StrDescripcion)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.IntAnio)
                .IsRequired();

            builder.Property(x => x.IntNumeroPeriodo)
                .IsRequired();

            builder.Property(x => x.DateFechaInicio)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.DateFechaFin)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            builder.Property(x => x.IdTipoPeriodo)
                .IsRequired();

            /* Un tipo de periodo puede estar relacionado
            con varios periodos. */
            builder.HasOne(x => x.TipoPeriodo)
                .WithMany()
                .HasForeignKey(x => x.IdTipoPeriodo)
                .OnDelete(DeleteBehavior.Restrict);

            /* Evita duplicar el mismo número de periodo
            dentro de un mismo año. */
            builder.HasIndex(
                x => new
                {
                    x.IntAnio,
                    x.IntNumeroPeriodo
                })
                .IsUnique();
        }
    }
}
