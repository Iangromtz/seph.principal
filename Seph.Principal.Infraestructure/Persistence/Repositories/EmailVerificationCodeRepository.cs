using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Enums;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class EmailVerificationCodeRepository(ApplicationDbContext dbContext) : IEmailVerificationCodeRepository
    {
        /// <summary>
        /// Toma el código de verificación de correo electrónico activo más reciente para un usuario específico considerando su id,
        /// un código se considera activo si su estado es "Pendiente" y no ha expirado,
        /// si no se encuentra ningún código activo nos devuelve null.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<EmailVerificationCode?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => dbContext.EmailVerificationCodes
                .Where(code => code.UserId == userId
                    && code.Status == VerificationCodeStatus.Pending
                    && code.ExpiresAtUtc > DateTimeOffset.UtcNow)
                .OrderByDescending(code => code.CreatedAtUtc)
                .FirstOrDefaultAsync(cancellationToken);

        /// <summary>
        /// Toma todos los códigos de verificación de correo electrónico activos para un usuario específico considerando su id,
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<EmailVerificationCode>> GetAllActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await dbContext.EmailVerificationCodes
                .Where(code => code.UserId == userId && code.Status == VerificationCodeStatus.Pending)
                .ToListAsync(cancellationToken);

        /// <summary>
        /// Agrega un nuevo código de verificación de correo electrónico a la base de datos,
        /// este código se espera que tenga un estado "Pendiente" y una fecha de expiración futura poder cambiar a activo.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddAsync(EmailVerificationCode code, CancellationToken cancellationToken)
            => dbContext.EmailVerificationCodes.AddAsync(code, cancellationToken).AsTask();

        /// <summary>
        /// Actualiza un código de verificación de correo electrónico existente en la base de datos,
        /// este código se espera que tenga un estado "Pendiente" y una fecha de expiración futura poder cambiar a activo.
        /// </summary>
        /// <param name="code"></param>
        public void Update(EmailVerificationCode code)
            => dbContext.EmailVerificationCodes.Update(code);
    }
}