using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReportePersonal.DTOs
{
    public sealed record ReportePersonalEstadisticasDto(
        string Periodo,
        int TotalGeneral,
        int TotalDirectivos,
        int DirectivosHombres,
        int DirectivosMujeres,
        int TotalAdministrativos,
        int AdministrativosHombres,
        int AdministrativosMujeres,
        int TotalDocentes,
        int DocentesHombres,
        int DocentesMujeres,
        int DocentesTiempoCompleto,
        int DocentesAsignatura,
        int DocentesHora,
        string NivelAcademicoPredominante,
        decimal PorcentajeDirectivos,
        decimal PorcentajeAdministrativos,
        decimal PorcentajeDocentes,
        decimal PorcentajeDirectivosHombres,
        decimal PorcentajeDirectivosMujeres,
        decimal PorcentajeAdministrativosHombres,
        decimal PorcentajeAdministrativosMujeres,
        decimal PorcentajeDocentesHombres,
        decimal PorcentajeDocentesMujeres,
        decimal PorcentajeDocentesTiempoCompleto,
        decimal PorcentajeDocentesAsignatura,
        decimal PorcentajeDocentesHora);
}