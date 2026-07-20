using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    /// <summary>
    /// Configuración de la entidad ReporteInfraestructura
    /// para Entity Framework Core.
    /// </summary>
    public sealed class ReporteInfraestructuraConfiguration : IEntityTypeConfiguration<ReporteInfraestructura>
    {
        /// <summary>
        /// Configura la estructura de la tabla, sus propiedades
        /// obligatorias y el índice único de la entidad.
        /// </summary>
        public void Configure(EntityTypeBuilder<ReporteInfraestructura> builder)
        {
            // Nombre de la tabla en la base de datos.
            builder.ToTable("ReporteInfraestructura");

            // Llave primaria de la entidad.
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.IntTotalAulas)
                .IsRequired();

            builder.Property(x => x.IntTotalLaboratorios)
                .IsRequired();

            builder.Property(x => x.IntTotalTalleres)
                .IsRequired();

            builder.Property(x => x.BitBiblioteca)
                .IsRequired();

            builder.Property(x => x.IntTotalBibliotecas)
                .IsRequired();

            builder.Property(x => x.IntTotalComputo)
                .IsRequired();

            builder.Property(x => x.IdInternet)
                .IsRequired();

            builder.Property(x => x.IdDiscapacitado)
                .IsRequired();

            builder.Property(x => x.DateTimeFechaRegistro)
                .IsRequired();

            builder.Property(x => x.IdUsuarioRegistro)
                .IsRequired();

            builder.Property(x => x.BitActivo)
                .IsRequired();

            // Una institución únicamente puede tener
            // un reporte de infraestructura por periodo.
            builder.HasIndex(x => x.IdMapInstitucionPeriodo)
                .IsUnique();
        }
    }
}