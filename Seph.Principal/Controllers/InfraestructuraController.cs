using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.ReporteInfraestructura.Commands;
using Seph.Principal.Application.Features.ReporteInfraestructura.Queries.GetReporteInfraestructura;
using Seph.Principal.Application.Features.ReporteInfraestructura.Queries.GetReporteInfraestructuraComparativo;
using Seph.Principal.Application.Features.ReporteInfraestructura.Queries.GetReporteInfraestructuraEstadisticas;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class InfraestructuraController(IMediator mediator) : ControllerBase
    {
        // Registra el reporte de infraestructura.
        [HttpPost("reporte")]
        public async Task<IActionResult> CreateReporteInfraestructura(
            CreateReporteInfraestructuraCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Actualiza el reporte de infraestructura de un periodo institucional.
        [HttpPut("reporte")]
        public async Task<IActionResult> UpdateReporteInfraestructura(
            UpdateReporteInfraestructuraCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el reporte de infraestructura registrado para un periodo institucional.
        [HttpGet("reporte/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteInfraestructura(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteInfraestructuraQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el comparativo de infraestructura contra el periodo anterior.
        [HttpGet("reporte-comparativo/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteInfraestructuraComparativo(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteInfraestructuraComparativoQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene estadísticas listas para dashboard, gráficas o reportes.
        [HttpGet("estadisticas/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteInfraestructuraEstadisticas(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteInfraestructuraEstadisticasQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
    }
}