using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class CatNivelAcademicoConfiguration : IEntityTypeConfiguration<CatNivelAcademico>
    {
        public void Configure(EntityTypeBuilder<CatNivelAcademico> builder)
        {
            builder.ToTable("CatNivelAcademico");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.StrValor).HasMaxLength(200).IsRequired();
            builder.Property(x => x.StrDescripcion).HasMaxLength(500);
        }
    }
}