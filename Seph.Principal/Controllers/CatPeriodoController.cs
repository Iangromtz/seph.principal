using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.CatPeriodos.Queries.GetCatPeriodo;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class CatPeriodoController(IMediator mediator) : ControllerBase
    {
        // Obtiene el catálogo de periodos disponibles.
        [HttpGet]
        public async Task<IActionResult> GetCatPeriodo(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetCatPeriodoQuery(),
                cancellationToken);

            return Ok(response);
        }
    }
}
