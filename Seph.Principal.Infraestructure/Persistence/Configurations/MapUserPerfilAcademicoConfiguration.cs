using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class MapUserPerfilAcademicoConfiguration : IEntityTypeConfiguration<MapUserPerfilAcademico>
    {
        public void Configure(EntityTypeBuilder<MapUserPerfilAcademico> builder)
        {
            builder.ToTable("MapUserPerfilAcademico");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IdAspNetUsers).IsRequired();
            builder.Property(x => x.IdCatPerfilAcademico).IsRequired();
        }
    }
}
