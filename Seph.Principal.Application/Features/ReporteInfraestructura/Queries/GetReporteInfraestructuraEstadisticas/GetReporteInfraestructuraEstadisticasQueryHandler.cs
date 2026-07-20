using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteInfraestructura
    .Queries.GetReporteInfraestructuraEstadisticas
{
    /// <summary>
    /// Obtiene los indicadores de infraestructura
    /// correspondientes a una institución y periodo.
    /// </summary>
    public sealed class GetReporteInfraestructuraEstadisticasQueryHandler(
        IReporteInfraestructuraRepository reporteInfraestructuraRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository,
        ICatInternetRepository catInternetRepository,
        ICatDiscapacitadoRepository catDiscapacitadoRepository)
        : IRequestHandler<
            GetReporteInfraestructuraEstadisticasQuery,
            ResponseWrapper<ReporteInfraestructuraEstadisticasDto>>
    {
        /// <summary>
        /// Procesa la consulta de estadísticas de infraestructura.
        /// </summary>
        public async Task<ResponseWrapper<ReporteInfraestructuraEstadisticasDto>>
            Handle(
                GetReporteInfraestructuraEstadisticasQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene el reporte capturado para el periodo institucional.
            var reporte = await reporteInfraestructuraRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory
                    .Failure<ReporteInfraestructuraEstadisticasDto>(
                        "No existe un reporte de infraestructura para este periodo.",
                        HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo.
            var mapInstitucionPeriodo = await mapInstitucionPeriodoRepository
                .GetByIdAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (mapInstitucionPeriodo is null)
            {
                return ResponseFactory
                    .Failure<ReporteInfraestructuraEstadisticasDto>(
                        "No existe la relación institución-periodo.",
                        HttpStatusCode.NotFound);
            }

            // Obtiene la información del periodo seleccionado.
            var periodo = await catPeriodoRepository.GetByIdAsync(
                mapInstitucionPeriodo.IdPeriodo,
                cancellationToken);

            if (periodo is null)
            {
                return ResponseFactory
                    .Failure<ReporteInfraestructuraEstadisticasDto>(
                        "No existe el periodo seleccionado.",
                        HttpStatusCode.NotFound);
            }

            // Obtiene la descripción del catálogo de acceso a internet.
            var internet = await catInternetRepository.GetByIdAsync(
                reporte.IdInternet,
                cancellationToken);

            // Obtiene la descripción del catálogo de infraestructura
            // para personas con discapacidad.
            var discapacitado =
                await catDiscapacitadoRepository.GetByIdAsync(
                    reporte.IdDiscapacitado,
                    cancellationToken);

            var dto = new ReporteInfraestructuraEstadisticasDto(
                periodo.StrValor,
                reporte.IntTotalAulas,
                reporte.IntTotalLaboratorios,
                reporte.IntTotalTalleres,
                reporte.BitBiblioteca,
                reporte.IntTotalBibliotecas,
                reporte.IntTotalComputo,
                internet?.StrValor ?? "No definido",
                discapacitado?.StrValor ?? "No definido");

            return ResponseFactory.Success(
                dto,
                "Estadísticas de infraestructura obtenidas correctamente");
        }
    }
}