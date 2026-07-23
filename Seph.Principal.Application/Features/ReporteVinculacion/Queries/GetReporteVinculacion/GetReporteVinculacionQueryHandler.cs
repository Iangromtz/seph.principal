using System.Linq;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Queries.GetReporteVinculacion
{
    /// <summary>
    /// Procesa la consulta de un reporte de vinculación.
    /// </summary>
    public sealed class GetReporteVinculacionQueryHandler(
        IReporteVinculacionRepository reporteVinculacionRepository,
        ISectorVinculadoVinculacionRepository sectorVinculadoVinculacionRepository)
        : IRequestHandler<
            GetReporteVinculacionQuery,
            ResponseWrapper<ReporteVinculacionDto>>
    {
        /// <summary>
        /// Obtiene el reporte de vinculación correspondiente
        /// a una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReporteVinculacionDto>> Handle(
            GetReporteVinculacionQuery request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte registrado para el periodo institucional seleccionado.
            var reporte = await reporteVinculacionRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteVinculacionDto>(
                    "No existe un reporte de vinculación para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene los sectores asociados al reporte de vinculación.
            var sectoresVinculados =
                await sectorVinculadoVinculacionRepository
                    .GetByIdVinculacionAsync(
                        reporte.Id,
                        cancellationToken);

            var dto =
                new ReporteVinculacionDto
                {
                    Id =
                        reporte.Id,

                    IdMapInstitucionPeriodo =
                        reporte.IdMapInstitucionPeriodo,

                    IntTotalConveniosActivos =
                        reporte.IntTotalConveniosActivos,

                    BitPracticasProfesionales =
                        reporte.BitPracticasProfesionales,

                    BitServicioSocial =
                        reporte.BitServicioSocial,

                    BitSeguimientoEgresados =
                        reporte.BitSeguimientoEgresados,

                    IdMecanismoSeguimiento =
                        reporte.IdMecanismoSeguimiento,

                    DecimalPorcentajeLaborando =
                        reporte.DecimalPorcentajeLaborando,

                    SectoresVinculados =
                        sectoresVinculados
                            .Select(
                                sector =>
                                    new SectorVinculadoDto
                                    {
                                        IdSectorVinculado =
                                            sector.IdSectorVinculado,

                                        StrOtros =
                                            sector.StrOtros
                                    })
                            .ToList()
                };

            return ResponseFactory.Success(
                dto,
                "Reporte de vinculación obtenido correctamente");
        }
    }
}