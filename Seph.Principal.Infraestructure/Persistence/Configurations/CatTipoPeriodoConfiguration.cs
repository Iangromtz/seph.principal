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
    public sealed class CatTipoPeriodoConfiguration
        : IEntityTypeConfiguration<CatTipoPeriodo>
    {
        public void Configure(
            EntityTypeBuilder<CatTipoPeriodo> builder)
        {
            builder.ToTable("CatTipoPeriodo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StrValor)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.StrDescripcion)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.IntNumeroMeses)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            /* Evita registrar dos tipos de periodo
            con el mismo valor. */
            builder.HasIndex(x => x.StrValor)
                .IsUnique();
        }
    }
}
