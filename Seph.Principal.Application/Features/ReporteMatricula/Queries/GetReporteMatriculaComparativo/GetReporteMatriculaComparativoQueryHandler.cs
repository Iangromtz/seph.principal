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

namespace Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaComparativo
{
    public sealed class GetReporteMatriculaComparativoQueryHandler(
          IReporteMatriculaRepository reporteMatriculaRepository,
          IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
          ICatPeriodoRepository catPeriodoRepository)
          : IRequestHandler<GetReporteMatriculaComparativoQuery, ResponseWrapper<ReporteMatriculaComparativoDto>>
    {
        public async Task<ResponseWrapper<ReporteMatriculaComparativoDto>> Handle(
            GetReporteMatriculaComparativoQuery request,
            CancellationToken cancellationToken)
        {
            // Obtiene el reporte actual.
            var reporteActual = await reporteMatriculaRepository.GetByMapInstitucionPeriodoAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (reporteActual is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaComparativoDto>(
                    "No existe un reporte de matrícula para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo actual.
            var mapActual = await mapInstitucionPeriodoRepository.GetByIdAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (mapActual is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaComparativoDto>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene los datos del periodo actual.
            var periodoActual = await catPeriodoRepository.GetByIdAsync(
                mapActual.IdPeriodo,
                cancellationToken);

            if (periodoActual is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaComparativoDto>(
                    "No existe el periodo actual.",
                    HttpStatusCode.NotFound);
            }

            // Busca el reporte anterior de la misma institución.
            var reporteAnterior = await reporteMatriculaRepository.GetPreviousReporteAsync(
                mapActual.IdInstitucion,
                periodoActual.IntAnio,
                periodoActual.IntNumeroPeriodo,
                cancellationToken);

            if (reporteAnterior is null)
            {
                var sinAnterior = new ReporteMatriculaComparativoDto(
                    periodoActual.StrValor,
                    reporteActual.IntTotal,
                    null,
                    null,
                    0,
                    0,
                    "Sin periodo anterior");

                return ResponseFactory.Success(
                    sinAnterior,
                    "No existe un periodo anterior para comparar.");
            }

            var mapAnterior = await mapInstitucionPeriodoRepository.GetByIdAsync(
                reporteAnterior.IdMapInstitucionPeriodo,
                cancellationToken);

            var periodoAnterior = mapAnterior is null
                ? null
                : await catPeriodoRepository.GetByIdAsync(
                    mapAnterior.IdPeriodo,
                    cancellationToken);

            var diferencia = reporteActual.IntTotal - reporteAnterior.IntTotal;

            var porcentaje = reporteAnterior.IntTotal == 0
                ? 0
                : Math.Round((decimal)diferencia / reporteAnterior.IntTotal * 100, 2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            var dto = new ReporteMatriculaComparativoDto(
                periodoActual.StrValor,
                reporteActual.IntTotal,
                periodoAnterior?.StrValor,
                reporteAnterior.IntTotal,
                diferencia,
                porcentaje,
                estado);

            return ResponseFactory.Success(
                dto,
                "Comparativo de matrícula obtenido correctamente");
        }
    }
}
