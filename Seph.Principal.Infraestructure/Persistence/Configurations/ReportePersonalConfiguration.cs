using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class ReportePersonalConfiguration : IEntityTypeConfiguration<ReportePersonal>
    {
        public void Configure(EntityTypeBuilder<ReportePersonal> builder)
        {
            builder.ToTable("ReportePersonal");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IdMapInstitucionPeriodo)
                .IsRequired();

            builder.Property(x => x.IntTotalGeneral)
                .IsRequired();

            builder.Property(x => x.IntTotalDirectivos)
                .IsRequired();

            builder.Property(x => x.IntTotalDirectivosHombres)
                .IsRequired();

            builder.Property(x => x.IntTotalDirectivosMujeres)
                .IsRequired();

            builder.Property(x => x.IntTotalAdministrativos)
                .IsRequired();

            builder.Property(x => x.IntTotalAdministrativosHombres)
                .IsRequired();

            builder.Property(x => x.IntTotalAdministrativosMujeres)
                .IsRequired();

            builder.Property(x => x.IntTotalDocentes)
                .IsRequired();

            builder.Property(x => x.IntTotalDocentesHombres)
                .IsRequired();

            builder.Property(x => x.IntTotalDocentesMujeres)
                .IsRequired();

            builder.Property(x => x.IntTotalDocentesTiempoCompleto)
                .IsRequired();

            builder.Property(x => x.IntTotalDocentesAsignatura)
                .IsRequired();

            builder.Property(x => x.IntTotalDocentesHora)
                .IsRequired();

            builder.Property(x => x.IdNivelAcademico)
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