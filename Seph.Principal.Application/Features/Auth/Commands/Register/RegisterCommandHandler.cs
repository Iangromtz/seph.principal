using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Common.Security;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.Register
{
    public sealed class RegisterCommandHandler(
        IIdentityService identityService,
        IEmailVerificationCodeRepository verificationCodeRepository,
        IJwtTokenService jwtTokenService,
        IEmailService emailService,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<RegisterCommand, ResponseWrapper<string>>
    {
        /// <summary>
        /// Maneja el proceso de registro de un nuevo usuario, registra al usuario y genera un código OTP para verificación de correo,
        /// lo guarda en la base de datos y envía un correo con el código al usuario.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseWrapper<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Registramos el usuario
            var userId = await identityService.RegisterAsync(
              request.FullName, request.Email, request.Password, cancellationToken);

            // Si el correo está en uso returna "Failure" con status 409 Conflict
            if (userId is null)
                return ResponseFactory.Failure<string>("El correo ya está registrado", HttpStatusCode.Conflict);

            // Generación del código de verificación OTP
            var code = OtpGenerator.Generate(5);
            // Hash del código
            var codeHash = jwtTokenService.HashRefreshToken(code);
            // Establecemos tiempo de expiración
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(15);

            // Creamos el código
            var verificationCode = EmailVerificationCode.Create(userId.Value, codeHash, expiresAt);
            // Guardamos el código en la base de datos
            await verificationCodeRepository.AddAsync(verificationCode, cancellationToken);
            // Guardamos los cambios
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Enviar correo de verificación
            await emailService.SendEmailAsync(
                request.Email,
                "Verifica tu correo — Seph",
                BuildEmailBody(request.FullName, code),
                cancellationToken);

            return ResponseFactory.Success("Registro exitoso. Revisa tu correo para verificar tu cuenta.");
        }

        /// <summary>
        /// Construye el cuerpo del correo electrónico de verificación incluyendo datos de incluyendo datos del usuario y el código OTP.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string BuildEmailBody(string fullName, string code) => $"""
            <div style="font-family: Arial, sans-serif; max-width: 480px; margin: auto; padding: 32px; border: 1px solid #e5e7eb; border-radius: 8px;">
              <h2 style="color: #1e293b;">Hola, {fullName} 👋</h2>
              <p style="color: #475569;">Usa el siguiente código para verificar tu correo electrónico:</p>
              <div style="text-align: center; margin: 32px 0;">
                <span style="font-size: 36px; font-weight: bold; letter-spacing: 8px; color: #4f46e5;">{code}</span>
              </div>
              <p style="color: #94a3b8; font-size: 13px;">Este código expira en <strong>15 minutos</strong>. Si no creaste una cuenta, ignora este correo.</p>
            </div>
            """;
    }
}
