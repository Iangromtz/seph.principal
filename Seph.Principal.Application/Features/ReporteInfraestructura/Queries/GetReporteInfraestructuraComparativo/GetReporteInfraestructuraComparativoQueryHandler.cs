using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteInfraestructura
    .Queries.GetReporteInfraestructuraComparativo
{
    /// <summary>
    /// Obtiene el comparativo de cada indicador de infraestructura
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed class GetReporteInfraestructuraComparativoQueryHandler(
        IReporteInfraestructuraRepository reporteInfraestructuraRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteInfraestructuraComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<ReporteInfraestructuraComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<ReporteInfraestructuraComparativoDto>>>
            Handle(
                GetReporteInfraestructuraComparativoQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene el reporte de infraestructura del periodo actual.
            var reporteActual = await reporteInfraestructuraRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporteActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReporteInfraestructuraComparativoDto>>(
                    "No existe un reporte de infraestructura para este periodo.",
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
                    IReadOnlyCollection<ReporteInfraestructuraComparativoDto>>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la información del periodo actual.
            var periodoActual = await catPeriodoRepository.GetByIdAsync(
                mapActual.IdPeriodo,
                cancellationToken);

            if (periodoActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReporteInfraestructuraComparativoDto>>(
                    "No existe el periodo actual.",
                    HttpStatusCode.NotFound);
            }

            // Busca el reporte anterior de la misma institución.
            var reporteAnterior = await reporteInfraestructuraRepository
                .GetPreviousReporteAsync(
                    mapActual.IdInstitucion,
                    periodoActual.IntAnio,
                    periodoActual.IntNumeroPeriodo,
                    cancellationToken);

            if (reporteAnterior is null)
            {
                IReadOnlyCollection<ReporteInfraestructuraComparativoDto>
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

            IReadOnlyCollection<ReporteInfraestructuraComparativoDto>
                comparativos = new List<ReporteInfraestructuraComparativoDto>
                {
                    CrearComparativo(
                        "Aulas",
                        periodoActual.StrValor,
                        reporteActual.IntTotalAulas,
                        periodoAnterior?.StrValor,
                        reporteAnterior.IntTotalAulas),

                    CrearComparativo(
                        "Laboratorios",
                        periodoActual.StrValor,
                        reporteActual.IntTotalLaboratorios,
                        periodoAnterior?.StrValor,
                        reporteAnterior.IntTotalLaboratorios),

                    CrearComparativo(
                        "Talleres",
                        periodoActual.StrValor,
                        reporteActual.IntTotalTalleres,
                        periodoAnterior?.StrValor,
                        reporteAnterior.IntTotalTalleres),

                    CrearComparativo(
                        "Bibliotecas",
                        periodoActual.StrValor,
                        reporteActual.IntTotalBibliotecas,
                        periodoAnterior?.StrValor,
                        reporteAnterior.IntTotalBibliotecas),

                    CrearComparativo(
                        "Equipos de cómputo",
                        periodoActual.StrValor,
                        reporteActual.IntTotalComputo,
                        periodoAnterior?.StrValor,
                        reporteAnterior.IntTotalComputo)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de infraestructura obtenido correctamente");
        }

        /// <summary>
        /// Construye el comparativo de un indicador de infraestructura.
        /// </summary>
        private static ReporteInfraestructuraComparativoDto CrearComparativo(
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

            return new ReporteInfraestructuraComparativoDto(
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
        /// Construye los indicadores cuando no existe
        /// un reporte correspondiente al periodo anterior.
        /// </summary>
        private static IReadOnlyCollection<
            ReporteInfraestructuraComparativoDto>
            CrearComparativosSinPeriodoAnterior(
                string periodoActual,
                Domain.Entities.ReporteInfraestructura reporteActual)
        {
            return new List<ReporteInfraestructuraComparativoDto>
            {
                CrearSinPeriodoAnterior(
                    "Aulas",
                    periodoActual,
                    reporteActual.IntTotalAulas),

                CrearSinPeriodoAnterior(
                    "Laboratorios",
                    periodoActual,
                    reporteActual.IntTotalLaboratorios),

                CrearSinPeriodoAnterior(
                    "Talleres",
                    periodoActual,
                    reporteActual.IntTotalTalleres),

                CrearSinPeriodoAnterior(
                    "Bibliotecas",
                    periodoActual,
                    reporteActual.IntTotalBibliotecas),

                CrearSinPeriodoAnterior(
                    "Equipos de cómputo",
                    periodoActual,
                    reporteActual.IntTotalComputo)
            };
        }

        /// <summary>
        /// Construye un indicador cuando no existe un periodo anterior.
        /// </summary>
        private static ReporteInfraestructuraComparativoDto
            CrearSinPeriodoAnterior(
                string indicador,
                string periodoActual,
                int valorActual)
        {
            return new ReporteInfraestructuraComparativoDto(
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