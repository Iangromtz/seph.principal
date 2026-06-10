using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.Register
{
    public sealed class RegisterCommandHandler(
      IIdentityService identityService
  ) : IRequestHandler<RegisterCommand, ResponseWrapper<Guid>>
    {
        public async Task<ResponseWrapper<Guid>> Handle(
            RegisterCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.RegisterAsync(
                request.FullName, request.Email, request.Password, cancellationToken);

            if (userId is null)
                return ResponseFactory.Failure<Guid>(
                    "No se pudo crear el usuario. El correo ya puede estar registrado.",
                    HttpStatusCode.Conflict);

            return ResponseFactory.Success(userId.Value, "Usuario registrado correctamente.");
        }
    }
}
