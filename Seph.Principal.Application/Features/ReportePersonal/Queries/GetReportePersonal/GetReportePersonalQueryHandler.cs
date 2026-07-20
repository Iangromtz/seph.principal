using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonal
{
    public sealed class GetReportePersonalQueryHandler(
        IReportePersonalRepository reportePersonalRepository)
        : IRequestHandler<
            GetReportePersonalQuery,
            ResponseWrapper<ReportePersonalDto>>
    {
        public async Task<ResponseWrapper<ReportePersonalDto>> Handle(
            GetReportePersonalQuery request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte registrado para el periodo institucional seleccionado.
            var reporte = await reportePersonalRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReportePersonalDto>(
                    "No existe un reporte de personal para este periodo.",
                    HttpStatusCode.NotFound);
            }

            var dto = new ReportePersonalDto(
                reporte.Id,
                reporte.IdMapInstitucionPeriodo,
                reporte.IntTotalGeneral,
                reporte.IntTotalDirectivos,
                reporte.IntTotalDirectivosHombres,
                reporte.IntTotalDirectivosMujeres,
                reporte.IntTotalAdministrativos,
                reporte.IntTotalAdministrativosHombres,
                reporte.IntTotalAdministrativosMujeres,
                reporte.IntTotalDocentes,
                reporte.IntTotalDocentesHombres,
                reporte.IntTotalDocentesMujeres,
                reporte.IntTotalDocentesTiempoCompleto,
                reporte.IntTotalDocentesAsignatura,
                reporte.IntTotalDocentesHora,
                reporte.IdNivelAcademico,
                reporte.DateTimeFechaRegistro,
                reporte.IdUsuarioRegistro,
                reporte.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de personal obtenido correctamente");
        }
    }
}