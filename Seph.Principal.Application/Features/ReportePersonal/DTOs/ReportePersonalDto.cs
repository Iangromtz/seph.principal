using System;

namespace Seph.Principal.Application.Features.ReportePersonal.DTOs
{
    public sealed record ReportePersonalDto(
        long Id,
        long IdMapInstitucionPeriodo,
        int IntTotalGeneral,
        int IntTotalDirectivos,
        int IntTotalDirectivosHombres,
        int IntTotalDirectivosMujeres,
        int IntTotalAdministrativos,
        int IntTotalAdministrativosHombres,
        int IntTotalAdministrativosMujeres,
        int IntTotalDocentes,
        int IntTotalDocentesHombres,
        int IntTotalDocentesMujeres,
        int IntTotalDocentesTiempoCompleto,
        int IntTotalDocentesAsignatura,
        int IntTotalDocentesHora,
        long IdNivelAcademico,
        DateTime DateTimeFechaRegistro,
        Guid IdUsuarioRegistro,
        bool BitActivo);
}