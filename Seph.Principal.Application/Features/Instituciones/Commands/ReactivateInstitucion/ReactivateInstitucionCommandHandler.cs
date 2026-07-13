using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Instituciones.Commands.ReactivateInstitucion
{
    public sealed class ReactivateInstitucionCommandHandler(
        IInstitucionRepository institucionRepository,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork) : IRequestHandler<ReactivateInstitucionCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(
            ReactivateInstitucionCommand request,
            CancellationToken cancellationToken)
        {
            var institucion = await institucionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (institucion is null)
            {
                return ResponseFactory.Failure<string>("Institución no encontrada", HttpStatusCode.NotFound);
            }

            institucion.BitActivo = true;
            institucion.DateTimeFechaBaja = null;

            institucionRepository.Update(institucion);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await bitacoraService.RegistrarAsync(
                "Institucion",
                institucion.Id.ToString(),
                "Activar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email ?? "desconocido",
                institucion,
                cancellationToken);

            return ResponseFactory.Success(
                "Institución reactivada correctamente",
                "Institución reactivada correctamente");
        }
    }
}
