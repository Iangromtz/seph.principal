using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatricula
{
    public sealed class GetReporteMatriculaQueryHandler(
         IReporteMatriculaRepository reporteMatriculaRepository)
         : IRequestHandler<GetReporteMatriculaQuery, ResponseWrapper<ReporteMatriculaDto>>
    {
        public async Task<ResponseWrapper<ReporteMatriculaDto>> Handle(
            GetReporteMatriculaQuery request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte registrado para el periodo institucional seleccionado.
            var reporte = await reporteMatriculaRepository.GetByMapInstitucionPeriodoAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaDto>(
                    "No existe un reporte de matrícula para este periodo.",
                    HttpStatusCode.NotFound);
            }

            var dto = new ReporteMatriculaDto(
                reporte.Id,
                reporte.IdMapInstitucionPeriodo,
                reporte.IntTotal,
                reporte.IntTotalHombres,
                reporte.IntTotalMujeres,
                reporte.IntTsu,
                reporte.IntLicenciatura,
                reporte.IntPostgrado,
                reporte.DecimalTazaDesercion,
                reporte.DecimalTazaReprobacion,
                reporte.DecimalTazaEficienciaTerminal,
                reporte.DateTimeFechaRegistro,
                reporte.IdUsuarioRegistro,
                reporte.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de matrícula obtenido correctamente");
        }
    }
}
