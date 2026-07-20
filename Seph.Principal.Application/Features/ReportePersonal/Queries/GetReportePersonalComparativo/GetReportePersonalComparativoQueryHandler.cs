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

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalComparativo
{
    public sealed class GetReportePersonalComparativoQueryHandler(
        IReportePersonalRepository reportePersonalRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReportePersonalComparativoQuery,
            ResponseWrapper<ReportePersonalComparativoDto>>
    {
        public async Task<ResponseWrapper<ReportePersonalComparativoDto>> Handle(
            GetReportePersonalComparativoQuery request,
            CancellationToken cancellationToken)
        {
            // Obtiene el reporte actual.
            var reporteActual = await reportePersonalRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporteActual is null)
            {
                return ResponseFactory.Failure<ReportePersonalComparativoDto>(
                    "No existe un reporte de personal para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo actual.
            var mapActual = await mapInstitucionPeriodoRepository.GetByIdAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (mapActual is null)
            {
                return ResponseFactory.Failure<ReportePersonalComparativoDto>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene los datos del periodo actual.
            var periodoActual = await catPeriodoRepository.GetByIdAsync(
                mapActual.IdPeriodo,
                cancellationToken);

            if (periodoActual is null)
            {
                return ResponseFactory.Failure<ReportePersonalComparativoDto>(
                    "No existe el periodo actual.",
                    HttpStatusCode.NotFound);
            }

            // Busca el reporte anterior de la misma institución.
            var reporteAnterior = await reportePersonalRepository
                .GetPreviousReporteAsync(
                    mapActual.IdInstitucion,
                    periodoActual.IntAnio,
                    periodoActual.IntNumeroPeriodo,
                    cancellationToken);

            if (reporteAnterior is null)
            {
                var sinAnterior = new ReportePersonalComparativoDto(
                    periodoActual.StrValor,
                    reporteActual.IntTotalGeneral,
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

            var diferencia =
                reporteActual.IntTotalGeneral -
                reporteAnterior.IntTotalGeneral;

            var porcentaje = reporteAnterior.IntTotalGeneral == 0
                ? 0
                : Math.Round(
                    (decimal)diferencia /
                    reporteAnterior.IntTotalGeneral *
                    100,
                    2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            var dto = new ReportePersonalComparativoDto(
                periodoActual.StrValor,
                reporteActual.IntTotalGeneral,
                periodoAnterior?.StrValor,
                reporteAnterior.IntTotalGeneral,
                diferencia,
                porcentaje,
                estado);

            return ResponseFactory.Success(
                dto,
                "Comparativo de personal obtenido correctamente");
        }
    }
}