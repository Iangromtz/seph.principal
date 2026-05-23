using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandHandler(IIdentityService identityService,
    IJwtTokenService jwtTokenService,
    IRefreshTokenSessionRepository sessionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RefreshTokenCommand, ResponseWrapper<AuthResponseDto>>
    {
        public async Task<ResponseWrapper<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            /*1. Validar el token de refresco utilizando el IJwtTokenService
             * para asegurarse de que sea válido y no haya expirado. */
            var tokenHash = jwtTokenService.HashRefreshToken(request.RefreshToken);
            /*2. Recuperar la sesión de usuario asociada al token de refresco desde el IRefreshTokenSessionRepository.
             * Verificar que la sesión esté activa y que el token de refresco coincida con el hash almacenado. */
            var session = await sessionRepository.GetActiveByTokenHashAsync(tokenHash, cancellationToken);
            /*3. Si la sesión es válida, generar un nuevo token de acceso y un nuevo token de refresco utilizando el IJwtTokenService.
             * Actualizar la sesión de usuario con el nuevo hash del token de refresco y la nueva fecha de expiración. */
            if (session is null || !session.IsActive || session.DeviceId != request.DeviceId)
            {
                return ResponseFactory.Failure<AuthResponseDto>("Sesión inválida o expirada",HttpStatusCode.Code.Unauthorized)
            }



        }
    }
    
}
