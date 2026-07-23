using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionComparativo
{
    /// <summary>
    /// Obtiene el comparativo de convenios activos
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed class GetReporteVinculacionComparativoQueryHandler(
        IReporteVinculacionRepository reporteVinculacionRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteVinculacionComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<ReporteVinculacionComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<ReporteVinculacionComparativoDto>>>
            Handle(
                GetReporteVinculacionComparativoQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene el reporte de vinculación del periodo actual.
            var reporteActual = await reporteVinculacionRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporteActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReporteVinculacionComparativoDto>>(
                    "No existe un reporte de vinculación para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo actual.
            var mapActual = await mapInstitucionPeriodoRepository
                .GetByIdAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (mapActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReporteVinculacionComparativoDto>>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la información del periodo actual.
            var periodoActual = await catPeriodoRepository
                .GetByIdAsync(
                    mapActual.IdPeriodo,
                    cancellationToken);

            if (periodoActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReporteVinculacionComparativoDto>>(
                    "No existe el periodo actual.",
                    HttpStatusCode.NotFound);
            }

            // Busca el reporte anterior de la misma institución.
            var reporteAnterior = await reporteVinculacionRepository
                .GetPreviousReporteAsync(
                    mapActual.IdInstitucion,
                    periodoActual.IntAnio,
                    periodoActual.IntNumeroPeriodo,
                    cancellationToken);

            if (reporteAnterior is null)
            {
                IReadOnlyCollection<ReporteVinculacionComparativoDto>
                    comparativosSinAnterior =
                    CrearComparativosSinPeriodoAnterior(
                        periodoActual.StrValor,
                        reporteActual);

                return ResponseFactory.Success(
                    comparativosSinAnterior,
                    "No existe un periodo anterior para comparar.");
            }

            var mapAnterior = await mapInstitucionPeriodoRepository
                .GetByIdAsync(
                    reporteAnterior.IdMapInstitucionPeriodo,
                    cancellationToken);

            var periodoAnterior = mapAnterior is null
                ? null
                : await catPeriodoRepository.GetByIdAsync(
                    mapAnterior.IdPeriodo,
                    cancellationToken);

            IReadOnlyCollection<ReporteVinculacionComparativoDto>
                comparativos = new List<ReporteVinculacionComparativoDto>
                {
                    CrearComparativo(
                        "Convenios activos",
                        periodoActual.StrValor,
                        reporteActual.IntTotalConveniosActivos,
                        periodoAnterior?.StrValor,
                        reporteAnterior.IntTotalConveniosActivos)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de vinculación obtenido correctamente");
        }

        /// <summary>
        /// Construye el comparativo de convenios activos.
        /// </summary>
        private static ReporteVinculacionComparativoDto CrearComparativo(
            string indicador,
            string periodoActual,
            int valorActual,
            string? periodoAnterior,
            int valorAnterior)
        {
            var diferencia = valorActual - valorAnterior;

            var porcentaje = valorAnterior == 0
                ? 0
                : Math.Round(
                    (decimal)diferencia /
                    valorAnterior *
                    100,
                    2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            return new ReporteVinculacionComparativoDto(
                indicador,
                periodoActual,
                valorActual,
                periodoAnterior,
                valorAnterior,
                diferencia,
                porcentaje,
                estado);
        }

        /// <summary>
        /// Construye el indicador cuando no existe
        /// un reporte correspondiente al periodo anterior.
        /// </summary>
        private static IReadOnlyCollection<
            ReporteVinculacionComparativoDto>
            CrearComparativosSinPeriodoAnterior(
                string periodoActual,
                Domain.Entities.ReporteVinculacion reporteActual)
        {
            return new List<ReporteVinculacionComparativoDto>
            {
                CrearSinPeriodoAnterior(
                    "Convenios activos",
                    periodoActual,
                    reporteActual.IntTotalConveniosActivos)
            };
        }

        /// <summary>
        /// Construye un indicador cuando no existe un periodo anterior.
        /// </summary>
        private static ReporteVinculacionComparativoDto
            CrearSinPeriodoAnterior(
                string indicador,
                string periodoActual,
                int valorActual)
        {
            return new ReporteVinculacionComparativoDto(
                indicador,
                periodoActual,
                valorActual,
                null,
                null,
                0,
                0,
                "Sin periodo anterior");
        }
    }
}