using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;

namespace Seph.Principal.Application.Features.ReportePersonal.Commands
{
    /*
     * Actualiza el reporte de personal asociado
     * a una relación institución-periodo.
     */
    public sealed record UpdateReportePersonalCommand(
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
        long IdNivelAcademico)
        : IRequest<ResponseWrapper<ReportePersonalDto>>;
}