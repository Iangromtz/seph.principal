using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;

namespace Seph.Principal.Application.Features.ReporteInfraestructura
    .Queries.GetReporteInfraestructuraComparativo
{
    /// <summary>
    /// Solicita el comparativo de los indicadores de infraestructura
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed record GetReporteInfraestructuraComparativoQuery(
        long IdMapInstitucionPeriodo): IRequest<ResponseWrapper<IReadOnlyCollection<ReporteInfraestructuraComparativoDto>>>;
}