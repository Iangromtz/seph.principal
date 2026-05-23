using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Auth.Commands.RevokeSession
{
    public sealed class RevokeSessionCommandHandler(IJwtTokenService jwtTokenService,
    IRefreshTokenSessionRepository sessionRepository,
    IUnitOfWork unitOfWork): IRequestHandler<RevokeSessionCommand, ResponseWrapper<bool>>
    {
        /*metodo que se encarga de revocar una sesión de usuario, invalidando el token de actualización proporcionado. 
         * El método busca la sesión correspondiente al token de actualización, verifica su validez y, si es válida, la revoca eliminándola de la base de datos. 
         * Finalmente, guarda los cambios y devuelve un resultado indicando si la revocación fue exitosa o no.*/
        public async Task<ResponseWrapper<bool>> Handle(RevokeSessionCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = jwtTokenService.HashRefreshToken(request.RefreshToken);
            var session = await sessionRepository.GetActiveByTokenHashAsync(tokenHash, cancellationToken);
            if (session is not null) 
            {
                session.Revoke();
                sessionRepository.Update(session);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return ResponseFactory.Success(true, "Sesión cerrada exitosamente");
        }
    }
}
