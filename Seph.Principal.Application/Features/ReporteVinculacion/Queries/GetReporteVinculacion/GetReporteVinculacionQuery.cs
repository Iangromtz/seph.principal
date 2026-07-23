using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Queries.GetReporteVinculacion
{
    /// <summary>
    /// Obtiene el reporte de vinculación
    /// asociado a una institución y periodo.
    /// </summary>
    public sealed record GetReporteVinculacionQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReporteVinculacionDto>>;
}