using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Instituciones.Commands.UpdateInstitucion
{
    public sealed class UpdateInstitucionCommandHandler(
        IInstitucionRepository institucionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateInstitucionCommand, ResponseWrapper<InstitucionDto>>
    {
        public async Task<ResponseWrapper<InstitucionDto>> Handle(
            UpdateInstitucionCommand request,
            CancellationToken cancellationToken)
        {
            var institucion = await institucionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (institucion is null)
            {
                return ResponseFactory.Failure<InstitucionDto>("Institución no encontrada", HttpStatusCode.NotFound);
            }

            institucion.StrNombre = request.StrNombre;
            institucion.StrSiglas = request.StrSiglas;
            institucion.StrCct = request.StrCct;
            institucion.StrDireccion = request.StrDireccion;
            institucion.DateFechaCreacion = request.DateFechaCreacion;
            institucion.StrDecretoCreacion = request.StrDecretoCreacion;
            institucion.StrSitioWeb = request.StrSitioWeb;
            institucion.StrCorreoInstitucional = request.StrCorreoInstitucional;
            institucion.StrTelefonoInstitucional = request.StrTelefonoInstitucional;
            institucion.IdMunicipio = request.IdMunicipio;

            institucionRepository.Update(institucion);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new InstitucionDto(
                institucion.Id,
                institucion.StrNombre,
                institucion.StrSiglas,
                institucion.StrCct,
                institucion.StrDireccion,
                institucion.DateFechaCreacion,
                institucion.StrDecretoCreacion,
                institucion.StrSitioWeb,
                institucion.StrCorreoInstitucional,
                institucion.StrTelefonoInstitucional,
                institucion.IdMunicipio,
                institucion.BitActivo);

            return ResponseFactory.Success(dto, "Institución actualizada correctamente");
        }
    }
}
