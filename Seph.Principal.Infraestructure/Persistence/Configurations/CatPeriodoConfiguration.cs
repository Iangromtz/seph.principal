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
    public sealed class CatPeriodoConfiguration : IEntityTypeConfiguration<CatPeriodo>
    {
        public void Configure(EntityTypeBuilder<CatPeriodo> builder)
        {
            builder.ToTable("CatPeriodo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StrValor)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.IntAnio)
                .IsRequired();

            builder.Property(x => x.IntNumeroPeriodo)
                .IsRequired();

            builder.Property(x => x.DateFechaInicio)
                .IsRequired();

            builder.Property(x => x.DateFechaFin)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();
        }
    }
}
