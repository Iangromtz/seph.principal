using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion;
using Seph.Principal.Application.Features.Instituciones.Commands.DeactivateInstitucion;
using Seph.Principal.Application.Features.Instituciones.Commands.ReactivateInstitucion;
using Seph.Principal.Application.Features.Instituciones.Commands.UpdateInstitucion;
using Seph.Principal.Application.Features.Instituciones.DTOs;
using Seph.Principal.Application.Features.Instituciones.Queries.GetInstitucionById;
using Seph.Principal.Application.Features.Instituciones.Queries.GetInstituciones;

namespace Seph.Principal.Controllers
{
    public class InstitucionController(ISender sender) : ApiControllerBase
    {
        #region Create
        /// <summary>
        /// Solo el SuperAdmin crea instituciones.
        /// POST /api/v1/institucion/create-institucion
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-institucion")]
        public async Task<IActionResult> Create([FromBody] CreateInstitucionRequest request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                new CreateInstitucionCommand(
                    request.StrNombre,
                    request.StrSiglas,
                    request.StrCct,
                    request.StrDireccion,
                    request.DateFechaCreacion,
                    request.StrDecretoCreacion,
                    request.StrSitioWeb,
                    request.StrCorreoInstitucional,
                    request.StrTelefonoInstitucional,
                    request.IdMunicipio),
                cancellationToken);

            return FromResponse(response);
        }
        #endregion

        #region Update
        /// <summary>
        /// Solo el SuperAdmin edita instituciones.
        /// PUT /api/v1/institucion/{id}
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateInstitucionRequest request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                new UpdateInstitucionCommand(
                    id,
                    request.StrNombre,
                    request.StrSiglas,
                    request.StrCct,
                    request.StrDireccion,
                    request.DateFechaCreacion,
                    request.StrDecretoCreacion,
                    request.StrSitioWeb,
                    request.StrCorreoInstitucional,
                    request.StrTelefonoInstitucional,
                    request.IdMunicipio),
                cancellationToken);

            return FromResponse(response);
        }
        #endregion

        #region Deactivate
        /// <summary>
        /// Solo el SuperAdmin desactiva instituciones. No borra el registro
        /// (institucion.BitActivo = false), ya que puede tener empleados y
        /// contratos ya ligados a ella.
        /// PUT /api/v1/institucion/{id}/desactivar
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id:long}/desactivar")]
        public async Task<IActionResult> Deactivate(long id, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new DeactivateInstitucionCommand(id), cancellationToken);

            return FromResponse(response);
        }
        #endregion

        #region Reactivate
        /// <summary>
        /// Solo el SuperAdmin reactiva instituciones previamente desactivadas.
        /// PUT /api/v1/institucion/{id}/reactivar
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id:long}/reactivar")]
        public async Task<IActionResult> Reactivate(long id, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new ReactivateInstitucionCommand(id), cancellationToken);

            return FromResponse(response);
        }
        #endregion

        #region Get
        /// <summary>
        /// Lista todas las instituciones (para asignar Admins, llenar selects, etc.).
        /// GET /api/v1/institucion/get-instituciones
        /// </summary>
        [Authorize]
        [HttpGet("get-instituciones")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetInstitucionesQuery(), cancellationToken);

            return FromResponse(response);
        }

        /// <summary>
        /// Detalle de una institución. Se usa para poblar el formulario de edición.
        /// GET /api/v1/institucion/{id}
        /// </summary>
        [Authorize]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetInstitucionByIdQuery(id), cancellationToken);

            return FromResponse(response);
        }
        #endregion
    }
}
