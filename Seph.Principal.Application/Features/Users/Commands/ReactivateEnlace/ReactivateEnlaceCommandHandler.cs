using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Commands.ReactivateEnlace
{
    public sealed class ReactivateEnlaceCommandHandler(IIdentityService identityService)
        : IRequestHandler<ReactivateEnlaceCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(ReactivateEnlaceCommand request, CancellationToken cancellationToken)
        {
            var ok = await identityService.SetUserActiveStatusAsync(request.Id, true, cancellationToken);

            if (!ok)
            {
                return ResponseFactory.Failure<string>("Enlace académico no encontrado", HttpStatusCode.NotFound);
            }

            return ResponseFactory.Success(
                "Enlace académico reactivado correctamente",
                "Enlace académico reactivado correctamente");
        }
    }
}
