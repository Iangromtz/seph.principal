using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Application.Features.Empleados.Queries.GetRegistrosPersonal;
using System.Net;

namespace Seph.Principal.Controllers
{
    public class EmpleadoController(ISender sender, ICurrentUserService currentUserService) : ApiControllerBase
    {
        #region Create
        [AllowAnonymous]
        [HttpPost("create-empleado")]
        public async Task<IActionResult> Create([FromBody] CreateEmpleadoRequest request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new CreateEmpleadoCommand(
                request.StrNombre,
                request.StrApellidoPat,
                request.StrApellidoMat,
                request.StrCurp,
                request.IdSexo,
                request.IdInstitucion,
                request.DateTimeFechaRegistro,
                request.IdUsuarioRegistro,
                request.BitActivo,
                request.DateTimeFechaBaja
                ));

            return FromResponse(response);
        }
        #endregion

        #region Get
        /// <summary>
        /// Concentrado de registros de personal capturados por el usuario autenticado
        /// (empleado + historial de contrato + catálogos resueltos).
        /// GET /api/v1/empleado/get-registros
        /// </summary>
        [Authorize]
        [HttpGet("get-registros")]
        public async Task<IActionResult> GetRegistros(CancellationToken cancellationToken)
        {
            if (currentUserService.UserId is not { } userId)
            {
                return FromResponse(ResponseFactory.Failure<IReadOnlyList<RegistroPersonalDto>>(
                    "Usuario no autenticado", HttpStatusCode.Unauthorized));
            }

            return FromResponse(await sender.Send(
                new GetRegistrosPersonalQuery(userId), cancellationToken));
        }
        #endregion
    }
}
