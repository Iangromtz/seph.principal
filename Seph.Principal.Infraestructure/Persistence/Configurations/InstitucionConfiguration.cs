using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class InstitucionConfiguration : IEntityTypeConfiguration<Institucion>
    {
        public void Configure(
        EntityTypeBuilder<Institucion> builder)
        {
            builder.ToTable("Instituciones");

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
