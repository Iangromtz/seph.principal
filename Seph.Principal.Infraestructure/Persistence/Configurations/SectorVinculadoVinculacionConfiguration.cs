using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad
    /// SectorVinculadoVinculacion
    /// para Entity Framework Core.
    /// </summary>
    public sealed class SectorVinculadoVinculacionConfiguration
        : IEntityTypeConfiguration<SectorVinculadoVinculacion>
    {
        /// <summary>
        /// Configura la estructura de la tabla
        /// y sus propiedades obligatorias.
        /// </summary>
        public void Configure(
            EntityTypeBuilder<SectorVinculadoVinculacion> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("SectorVinculadoVinculacion");

            // Llave primaria.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdVinculacion)
                .IsRequired();

            builder.Property(x => x.IdSectorVinculado)
                .IsRequired();

            builder.Property(x => x.StrOtros)
                .HasMaxLength(500);
        }
    }
}