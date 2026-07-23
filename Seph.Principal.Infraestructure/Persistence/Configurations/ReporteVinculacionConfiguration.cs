using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad ReporteVinculacion
    /// para Entity Framework Core.
    /// </summary>
    public sealed class ReporteVinculacionConfiguration : IEntityTypeConfiguration<ReporteVinculacion>
    {
        /// <summary>
        /// Configura la estructura de la tabla, sus propiedades
        /// obligatorias y el índice único de la entidad.
        /// </summary>
        public void Configure(EntityTypeBuilder<ReporteVinculacion> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("ReporteVinculacion");

            // Llave primaria de la entidad.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.IntTotalConveniosActivos)
                .IsRequired();

            builder.Property(x => x.BitPracticasProfesionales)
                .IsRequired();

            builder.Property(x => x.BitServicioSocial)
                .IsRequired();

            builder.Property(x => x.BitSeguimientoEgresados)
                .IsRequired();

            builder.Property(x => x.DecimalPorcentajeLaborando)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            builder.Property(x => x.IdMecanismoSeguimiento);

            // Una institución únicamente puede tener
            // un reporte de vinculación por periodo.
            builder.HasIndex(x => x.IdMapInstitucionPeriodo)
                .IsUnique();
        }
    }
}