using Seph.Principal.Domain.Common;
using Seph.Principal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class EmailVerificationCode: AuditableEntity
    {
        #region Constructor
        public EmailVerificationCode()
        {

        }
        /// <summary>
        /// Constructor privado para forzar el uso del método de fábrica y garantizar la creación correcta de la entidad.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codeHash"></param>
        /// <param name="expiresAtUtc"></param>
        private EmailVerificationCode(Guid userId, string codeHash, DateTimeOffset expiresAtUtc)
        {
            UserId = userId;
            CodeHash = codeHash;
            ExpiresAtUtc = expiresAtUtc;
            Status = VerificationCodeStatus.Pending;
            Attempts = 0;
        }
        #endregion

        #region Propiedades
        public Guid UserId { get; private set; }
        public string CodeHash { get; private set; } = string.Empty;
        public DateTimeOffset ExpiresAtUtc { get; private set; }
        public int Attempts { get; private set; }
        public VerificationCodeStatus Status { get; private set; }
        public bool IsActive => Status == VerificationCodeStatus.Pending && ExpiresAtUtc > DateTimeOffset.UtcNow;
        #endregion

        #region Metodos de la clase
        /// <summary>
        /// Método para crear una nueva instancia de EmailVerificationCode que nos garantiza que la entidad se cree con un estado inicial válido.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codeHash"></param>
        /// <param name="expiresAtUtc"></param>
        /// <returns></returns>
        public static EmailVerificationCode Create(Guid userId, string codeHash, DateTimeOffset expiresAtUtc)
            => new(userId, codeHash, expiresAtUtc);

        /// <summary>
        /// Metodo para registrar el numero de intentos fallidos de verificacion.
        /// </summary>
        public void RegisterFailedAttempt()
        {
            Attempts++;
            Touch();
        }

        /// <summary>
        /// Metodo para marcar el codigo de verificacion si ya ha sido utilizado exitosamente para evitar su reutilización.
        /// </summary>
        public void MarkUsed()
        {
            Status = VerificationCodeStatus.Used;
            Touch();
        }


        /// <summary>
        /// Metodo para marcar el codigo de verificacion como expirado si ya pasó su fecha de expiración.
        /// </summary>
        public void Invalidate()
        {
            Status = VerificationCodeStatus.Expired;
            Touch();
        }
        #endregion
    }
}
