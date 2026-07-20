using System;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;

namespace Seph.Principal.Application.Features.ReportePersonal.Commands
{
    public sealed record CreateReportePersonalCommand(
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
        Guid IdUsuarioRegistro)
        : IRequest<ResponseWrapper<ReportePersonalDto>>
    {
    }
}