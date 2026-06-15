using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.VerifyEmail
{
    public sealed class VerifyEmailCommandHandler(
        IIdentityService identityService,
        IEmailVerificationCodeRepository verificationCodeRepository,
        IJwtTokenService jwtTokenService,
        IUnitOfWork unitOfWork) : IRequestHandler<VerifyEmailCommand, ResponseWrapper<string>>
    {
        private const int MaxAttempts = 5;

        public async Task<ResponseWrapper<string>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdByEmailAsync(request.Email, cancellationToken);
            if (userId is null)
                return ResponseFactory.Failure<string>("Correo no encontrado", HttpStatusCode.NotFound);

            if (await identityService.IsEmailConfirmedAsync(userId.Value, cancellationToken))
                return ResponseFactory.Failure<string>("El correo ya fue verificado", HttpStatusCode.Conflict);

            var session = await verificationCodeRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (session is null)
                return ResponseFactory.Failure<string>("El código expiró. Solicita uno nuevo.", HttpStatusCode.BadRequest);

            if (session.Attempts >= MaxAttempts)
            {
                session.Invalidate();
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseFactory.Failure<string>("Demasiados intentos. Solicita un nuevo código.", HttpStatusCode.TooManyRequests);
            }

            var codeHash = jwtTokenService.HashRefreshToken(request.Code.ToUpperInvariant());
            if (session.CodeHash != codeHash)
            {
                session.RegisterFailedAttempt();
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseFactory.Failure<string>(
                    $"Código incorrecto. Te quedan {MaxAttempts - session.Attempts} intentos.",
                    HttpStatusCode.BadRequest);
            }

            session.MarkUsed();
            await identityService.ConfirmEmailAsync(userId.Value, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success("Correo verificado correctamente. Ya puedes iniciar sesión.");
        }
    }
}