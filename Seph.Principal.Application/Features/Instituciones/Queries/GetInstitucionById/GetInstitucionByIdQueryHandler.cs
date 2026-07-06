using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Instituciones.Queries.GetInstitucionById
{
    public sealed class GetInstitucionByIdQueryHandler(IInstitucionRepository institucionRepository)
        : IRequestHandler<GetInstitucionByIdQuery, ResponseWrapper<InstitucionDto>>
    {
        public async Task<ResponseWrapper<InstitucionDto>> Handle(
            GetInstitucionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var institucion = await institucionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (institucion is null)
            {
                return ResponseFactory.Failure<InstitucionDto>("Institución no encontrada", HttpStatusCode.NotFound);
            }

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

            return ResponseFactory.Success(dto, "Institución obtenida correctamente");
        }
    }
}
