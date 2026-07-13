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
    public sealed class ReporteMatriculaConfiguration : IEntityTypeConfiguration<ReporteMatricula>
    {
        public void Configure(EntityTypeBuilder<ReporteMatricula> builder)
        {
            builder.ToTable("ReporteMatricula");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.IntTotal)
                .IsRequired();

            builder.Property(x => x.IntTotalHombres)
                .IsRequired();

            builder.Property(x => x.IntTotalMujeres)
                .IsRequired();

            builder.Property(x => x.IntTsu)
                .IsRequired();

            builder.Property(x => x.IntLicenciatura)
                .IsRequired();

            builder.Property(x => x.IntPostgrado)
                .IsRequired();

            builder.Property(x => x.DecimalTazaDesercion)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(x => x.DecimalTazaReprobacion)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(x => x.DecimalTazaEficienciaTerminal)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            builder.HasIndex(x => x.IdMapInstitucionPeriodo)
                .IsUnique();
        }
    }
}
