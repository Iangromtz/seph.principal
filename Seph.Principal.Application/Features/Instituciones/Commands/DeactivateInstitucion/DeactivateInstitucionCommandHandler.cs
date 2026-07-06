using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Instituciones.Commands.DeactivateInstitucion
{
    public sealed class DeactivateInstitucionCommandHandler(
        IInstitucionRepository institucionRepository,
        IIdentityService identityService,
        IUnitOfWork unitOfWork) : IRequestHandler<DeactivateInstitucionCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(
            DeactivateInstitucionCommand request,
            CancellationToken cancellationToken)
        {
            var institucion = await institucionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (institucion is null)
            {
                return ResponseFactory.Failure<string>("Institución no encontrada", HttpStatusCode.NotFound);
            }

            var tieneUsuariosActivos = await identityService.HasActiveUsersInInstitutionAsync(request.Id, cancellationToken);

            if (tieneUsuariosActivos)
            {
                return ResponseFactory.Failure<string>(
                    "No se puede desactivar: la institución tiene usuarios activos (administradores o personal) asignados.",
                    HttpStatusCode.Conflict);
            }

            institucion.BitActivo = false;
            institucion.DateTimeFechaBaja = DateTime.UtcNow;

            institucionRepository.Update(institucion);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success(
                "Institución desactivada correctamente",
                "Institución desactivada correctamente");
        }
    }
}
