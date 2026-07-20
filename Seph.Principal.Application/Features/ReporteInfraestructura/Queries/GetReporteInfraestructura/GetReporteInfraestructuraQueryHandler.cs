using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Queries.GetReporteInfraestructura
{
    /// <summary>
    /// Procesa la consulta de un reporte de infraestructura.
    /// </summary>
    public sealed class GetReporteInfraestructuraQueryHandler(
        IReporteInfraestructuraRepository reporteInfraestructuraRepository)
        : IRequestHandler<
            GetReporteInfraestructuraQuery,
            ResponseWrapper<ReporteInfraestructuraDto>>
    {
        /// <summary>
        /// Obtiene el reporte de infraestructura correspondiente
        /// a una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReporteInfraestructuraDto>> Handle(
            GetReporteInfraestructuraQuery request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte registrado para el periodo institucional seleccionado.
            var reporte = await reporteInfraestructuraRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteInfraestructuraDto>(
                    "No existe un reporte de infraestructura para este periodo.",
                    HttpStatusCode.NotFound);
            }

            var dto = new ReporteInfraestructuraDto(
                reporte.Id,
                reporte.IdMapInstitucionPeriodo,
                reporte.IntTotalAulas,
                reporte.IntTotalLaboratorios,
                reporte.IntTotalTalleres,
                reporte.BitBiblioteca,
                reporte.IntTotalBibliotecas,
                reporte.IntTotalComputo,
                reporte.IdInternet,
                reporte.IdDiscapacitado,
                reporte.DateTimeFechaRegistro,
                reporte.IdUsuarioRegistro,
                reporte.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de infraestructura obtenido correctamente");
        }
    }
}