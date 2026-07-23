using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;

namespace Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionEstadisticas
{
    /// <summary>
    /// Solicita las estadísticas de vinculación
    /// de una institución durante un periodo.
    /// </summary>
    public sealed record GetReporteVinculacionEstadisticasQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReporteVinculacionEstadisticasDto>>;
}