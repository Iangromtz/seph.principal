using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.CatPeriodos.Commands.ChangeStatusCatPeriodo;
using Seph.Principal.Application.Features.CatPeriodos.Commands.CreateCatPeriodo;
using Seph.Principal.Application.Features.CatPeriodos.Commands.UpdateCatPeriodo;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;
using Seph.Principal.Application.Features.CatPeriodos.Queries.GetCatPeriodo;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class CatPeriodoController(IMediator mediator) : ControllerBase
    {
        #region Create

        /// <summary>
        /// Solo el SuperAdmin registra nuevos periodos.
        /// El nombre visible se genera automáticamente a partir
        /// de las fechas de inicio y fin.
        /// POST /api/v1/CatPeriodo/create-periodo
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-periodo")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCatPeriodoRequest request,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new CreateCatPeriodoCommand(
                    request.IntAnio,
                    request.IntNumeroPeriodo,
                    request.DateFechaInicio,
                    request.IdTipoPeriodo,
                    request.DateFechaFin),
                cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion
        #region Update

        /// <summary>
        /// Actualiza un periodo existente.
        /// PUT /api/v1/CatPeriodo/{id}
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateCatPeriodoRequest request,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new UpdateCatPeriodoCommand(
                    id,
                    request.IntAnio,
                    request.IntNumeroPeriodo,
                    request.DateFechaInicio,
                    request.IdTipoPeriodo,
                    request.DateFechaFin),
                cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion
        #region ChangeStatus

        /// <summary>
        /// Activa o desactiva un periodo existente.
        /// PATCH /api/v1/CatPeriodo/{id}/status
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPatch("{id:long}/status")]
        public async Task<IActionResult> ChangeStatus(
            long id,
            [FromBody] ChangeStatusCatPeriodoRequest request,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new ChangeStatusCatPeriodoCommand(
                    id,
                    request.BitActivo),
                cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion
        #region Get

        /// <summary>
        /// Obtiene el catálogo de periodos registrados.
        /// GET /api/v1/CatPeriodo
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCatPeriodo(
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetCatPeriodoQuery(),
                cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion

    }
}
