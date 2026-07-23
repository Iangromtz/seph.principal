using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;

namespace Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionComparativo
{
    /// <summary>
    /// Solicita el comparativo de convenios activos
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed record GetReporteVinculacionComparativoQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<ReporteVinculacionComparativoDto>>>;
}