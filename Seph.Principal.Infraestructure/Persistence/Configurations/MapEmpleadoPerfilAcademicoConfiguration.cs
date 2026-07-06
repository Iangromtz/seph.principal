using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class MapEmpleadoPerfilAcademicoConfiguration : IEntityTypeConfiguration<MapEmpleadoPerfilAcademico>
    {
        public void Configure(
        EntityTypeBuilder<MapEmpleadoPerfilAcademico> builder)
        {
            builder.ToTable("MapEmpleadoPerfilAcademico");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdEmpleado)
                .IsRequired();

            builder.Property(x => x.IdCatPerfilAcademico)
                .IsRequired();
        }
    }
}
