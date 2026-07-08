using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Commands.DeactivateEnlace
{
    public sealed class DeactivateEnlaceCommandHandler(IIdentityService identityService)
        : IRequestHandler<DeactivateEnlaceCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(DeactivateEnlaceCommand request, CancellationToken cancellationToken)
        {
            var ok = await identityService.SetUserActiveStatusAsync(request.Id, false, cancellationToken);

            if (!ok)
            {
                return ResponseFactory.Failure<string>("Enlace académico no encontrado", HttpStatusCode.NotFound);
            }

            return ResponseFactory.Success(
                "Enlace académico desactivado correctamente",
                "Enlace académico desactivado correctamente");
        }
    }
}
