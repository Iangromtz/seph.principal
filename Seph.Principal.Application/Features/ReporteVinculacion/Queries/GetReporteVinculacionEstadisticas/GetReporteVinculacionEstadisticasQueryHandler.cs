using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionEstadisticas
{
    /// <summary>
    /// Obtiene los indicadores de vinculación
    /// correspondientes a una institución y periodo.
    /// </summary>
    public sealed class GetReporteVinculacionEstadisticasQueryHandler(
        IReporteVinculacionRepository reporteVinculacionRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteVinculacionEstadisticasQuery,
            ResponseWrapper<ReporteVinculacionEstadisticasDto>>
    {
        /// <summary>
        /// Procesa la consulta de estadísticas de vinculación.
        /// </summary>
        public async Task<ResponseWrapper<ReporteVinculacionEstadisticasDto>>
            Handle(
                GetReporteVinculacionEstadisticasQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene el reporte capturado para el periodo institucional.
            var reporte = await reporteVinculacionRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory
                    .Failure<ReporteVinculacionEstadisticasDto>(
                        "No existe un reporte de vinculación para este periodo.",
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
                    .Failure<ReporteVinculacionEstadisticasDto>(
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
                    .Failure<ReporteVinculacionEstadisticasDto>(
                        "No existe el periodo seleccionado.",
                        HttpStatusCode.NotFound);
            }

            var dto = new ReporteVinculacionEstadisticasDto(
                periodo.StrValor,
                reporte.IntTotalConveniosActivos);

            return ResponseFactory.Success(
                dto,
                "Estadísticas de vinculación obtenidas correctamente");
        }
    }
}