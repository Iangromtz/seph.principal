using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.ReportePersonal.Commands;
using Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonal;
using Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalComparativo;
using Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalEstadisticas;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class PersonalController(IMediator mediator) : ControllerBase
    {
        // Registra el reporte de personal.
        [HttpPost("reporte")]
        public async Task<IActionResult> CreateReportePersonal(
            CreateReportePersonalCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Actualiza el reporte de personal de un periodo institucional.
        [HttpPut("reporte")]
        public async Task<IActionResult> UpdateReportePersonal(
            UpdateReportePersonalCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el reporte de personal registrado para un periodo institucional.
        [HttpGet("reporte/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReportePersonal(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportePersonalQuery(idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el comparativo de personal contra el periodo anterior.
        [HttpGet("reporte-comparativo/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReportePersonalComparativo(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportePersonalComparativoQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene estadísticas listas para dashboard, gráficas o reportes.
        [HttpGet("estadisticas/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReportePersonalEstadisticas(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportePersonalEstadisticasQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
    }
}