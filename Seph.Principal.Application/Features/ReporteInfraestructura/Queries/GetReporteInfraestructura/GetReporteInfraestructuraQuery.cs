using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Queries.GetReporteInfraestructura
{
    /// <summary>
    /// Obtiene el reporte de infraestructura
    /// asociado a una institución y periodo.
    /// </summary>
    public sealed record GetReporteInfraestructuraQuery(long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReporteInfraestructuraDto>>;
}