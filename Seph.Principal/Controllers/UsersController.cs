using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Application.Features.Users.Commands.CreateAdmin;
using Seph.Principal.Application.Features.Users.Commands.CreateUser;
using Seph.Principal.Application.Features.Users.Commands.DeactivateEnlace;
using Seph.Principal.Application.Features.Users.Commands.ReactivateEnlace;
using Seph.Principal.Application.Features.Users.Commands.UpdateEnlace;
using Seph.Principal.Application.Features.Users.DTOs;
using Seph.Principal.Application.Features.Users.Queries.GetCurrentUser;
using Seph.Principal.Application.Features.Users.Queries.GetEnlaceAcademicoById;
using Seph.Principal.Application.Features.Users.Queries.GetEnlacesAcademicos;
using System.Net;

namespace Seph.Principal.Controllers
{
    [Authorize]
    public sealed class UsersController(ISender sender, ICurrentUserService currentUserService) : ApiControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken cancellationToken)
        {
            if (currentUserService.UserId is not { } userId)
            {
                return FromResponse(ResponseFactory.Failure<UserSessionDto>("Usuario no autenticado", HttpStatusCode.Unauthorized));
            }

            return FromResponse(await sender.Send(new GetCurrentUserQuery(userId), cancellationToken));
        }

        /// <summary>
        /// Metodo para crear un usuario administrador (SuperAdmin) ligado a una institución.
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("admins")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminRequest request, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(
                new CreateAdminCommand(request.FullName, request.Email, request.Password, request.IdInstitucion,
                    request.StrRutaIne, request.StrRutaFotografia, request.StrRFC, request.StrSNII,
                    request.IdNivelAcademico, request.IdsPerfilAcademico),
                cancellationToken));

        /// <summary>
        /// Metodo para crear un usuario ligado a la institución del administrador que realiza la petición.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (currentUserService.IdInstitucion is not { } idInstitucion)
            {
                return FromResponse(ResponseFactory.Failure<UserCreatedDto>(
                    "El administrador no tiene una institución asignada", HttpStatusCode.BadRequest));
            }

            return FromResponse(await sender.Send(
                new CreateUserCommand(request.FullName, request.Email, request.Password, idInstitucion),
                cancellationToken));
        }

        /// <summary>
        /// Concentrado de Enlaces Académicos (usuarios con rol Admin).
        /// GET /api/v1/users/admins
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("admins")]
        public async Task<IActionResult> GetEnlacesAcademicos(CancellationToken cancellationToken)
            => FromResponse(await sender.Send(new GetEnlacesAcademicosQuery(), cancellationToken));

        /// <summary>
        /// Detalle de un Enlace Académico (para poblar el formulario de edición).
        /// GET /api/v1/users/admins/{id}
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("admins/{id:guid}")]
        public async Task<IActionResult> GetEnlaceAcademicoById(Guid id, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(new GetEnlaceAcademicoByIdQuery(id), cancellationToken));

        /// <summary>
        /// Edita los datos de un Enlace Académico ya registrado.
        /// PUT /api/v1/users/admins/{id}
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("admins/{id:guid}")]
        public async Task<IActionResult> UpdateEnlace(Guid id, [FromBody] UpdateEnlaceRequest request, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(
                new UpdateEnlaceCommand(id, request.FullName, request.IdInstitucion,
                    request.StrRutaIne, request.StrRutaFotografia, request.StrRFC, request.StrSNII,
                    request.IdNivelAcademico, request.IdsPerfilAcademico),
                cancellationToken));

        /// <summary>
        /// Desactiva un Enlace Académico.
        /// PUT /api/v1/users/admins/{id}/desactivar
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("admins/{id:guid}/desactivar")]
        public async Task<IActionResult> DeactivateEnlace(Guid id, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(new DeactivateEnlaceCommand(id), cancellationToken));

        /// <summary>
        /// Reactiva un Enlace Académico previamente desactivado.
        /// PUT /api/v1/users/admins/{id}/reactivar
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("admins/{id:guid}/reactivar")]
        public async Task<IActionResult> ReactivateEnlace(Guid id, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(new ReactivateEnlaceCommand(id), cancellationToken));
    }

}
