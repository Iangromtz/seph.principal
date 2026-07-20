using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;

namespace Seph.Principal.Application.Features.ReporteInfraestructura
    .Queries.GetReporteInfraestructuraEstadisticas
{
    /// <summary>
    /// Solicita las estadísticas de infraestructura
    /// de una institución durante un periodo.
    /// </summary>
    public sealed record GetReporteInfraestructuraEstadisticasQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReporteInfraestructuraEstadisticasDto>>;
}