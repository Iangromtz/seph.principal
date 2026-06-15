using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IEmailVerificationCodeRepository
    {
        /// <summary>
        /// Obtiene el código de verificación de correo electrónico activo para un usuario específico considerando su id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EmailVerificationCode?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene todos los códigos de verificación de correo electrónico activos para un usuario específico considerando su id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<EmailVerificationCode>> GetAllActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        /// <summary>
        /// Agrega un nuevo código de verificación de correo electrónico a la base de datos.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(EmailVerificationCode code, CancellationToken cancellationToken);

        /// <summary>
        /// Actualiza un código de verificación de correo electrónico existente en la base de datos.
        /// </summary>
        /// <param name="code"></param>
        void Update(EmailVerificationCode code);
    }
}