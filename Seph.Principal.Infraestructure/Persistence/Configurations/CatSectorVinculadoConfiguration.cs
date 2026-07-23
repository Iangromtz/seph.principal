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
    public sealed class CatSectorVinculadoConfiguration : IEntityTypeConfiguration<CatSectorVinculado>
    {
        public void Configure(
        EntityTypeBuilder<CatSectorVinculado> builder)
        {
            builder.ToTable("CatSectorVinculado");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.StrValor)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.StrDescripcion)
                .HasMaxLength(450);
        }
    }
}
