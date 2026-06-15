using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Common.Security;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.ResendVerificationCode
{
    public sealed class ResendVerificationCodeCommandHandler(
        IIdentityService identityService,
        IEmailVerificationCodeRepository verificationCodeRepository,
        IJwtTokenService jwtTokenService,
        IEmailService emailService,
        IUnitOfWork unitOfWork) : IRequestHandler<ResendVerificationCodeCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(ResendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdByEmailAsync(request.Email, cancellationToken);
            if (userId is null)
                return ResponseFactory.Failure<string>("Correo no encontrado", HttpStatusCode.NotFound);

            if (await identityService.IsEmailConfirmedAsync(userId.Value, cancellationToken))
                return ResponseFactory.Failure<string>("El correo ya fue verificado", HttpStatusCode.Conflict);

            var activeCodes = await verificationCodeRepository.GetAllActiveByUserIdAsync(userId.Value, cancellationToken);
            foreach (var active in activeCodes)
                active.Invalidate();

            var code = OtpGenerator.Generate(5);
            var codeHash = jwtTokenService.HashRefreshToken(code);
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(15);

            var newCode = EmailVerificationCode.Create(userId.Value, codeHash, expiresAt);
            await verificationCodeRepository.AddAsync(newCode, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await emailService.SendEmailAsync(
                request.Email,
                "Tu nuevo código de verificación — Seph",
                BuildEmailBody(code),
                cancellationToken);

            return ResponseFactory.Success("Código reenviado. Revisa tu correo.");
        }

        private static string BuildEmailBody(string code) => $"""
            <div style="font-family: Arial, sans-serif; max-width: 480px; margin: auto; padding: 32px; border: 1px solid #e5e7eb; border-radius: 8px;">
              <h2 style="color: #1e293b;">Verifica tu correo</h2>
              <p style="color: #475569;">Aquí tienes tu nuevo código de verificación:</p>
              <div style="text-align: center; margin: 32px 0;">
                <span style="font-size: 36px; font-weight: bold; letter-spacing: 8px; color: #4f46e5;">{code}</span>
              </div>
              <p style="color: #94a3b8; font-size: 13px;">Este código expira en <strong>15 minutos</strong>.</p>
            </div>
            """;
    }
}