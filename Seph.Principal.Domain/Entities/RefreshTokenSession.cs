using Seph.Principal.Domain.Common;
using Seph.Principal.Domain.Enums;

namespace Seph.Principal.Domain.Entities
{
    /*clase que representa una sesión de token de actualización */
    public class RefreshTokenSession: AuditableEntity
    {
        #region Constructors
        public RefreshTokenSession()
        {
                
        }

        private RefreshTokenSession(Guid userId, string tokenHash, string deviceId, string ipAddress, DateTimeOffset expiresAtUtc)
        {
            UserId = userId;
            TokenHash = tokenHash;
            DeviceId = deviceId;
            IpAddress = ipAddress;
            ExpiresAtUtc = expiresAtUtc;
            Status = SessionStatus.Active;
        }
        #endregion

        #region Factory Method
        public Guid UserId { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;
        public string DeviceId { get; private set; } = string.Empty;
        public string IpAddress { get; private set; } = string.Empty;
        public DateTimeOffset ExpiresAtUtc { get; private set; }
        public DateTimeOffset? RevokedAtUtc { get; private set; }
        public SessionStatus Status { get; private set; }

        public bool IsActive => Status == SessionStatus.Active && ExpiresAtUtc > DateTimeOffset.UtcNow;
        #endregion

        #region Metodos de la clase
        public static RefreshTokenSession Create(Guid userId, string tokenHash, string deviceId, string ipAddress, DateTimeOffset expiresAtUtc)
       => new(userId, tokenHash, deviceId, ipAddress, expiresAtUtc);

        public void Rotate(string tokenHash, DateTimeOffset expiresAtUtc, string ipAddress)
        {
            TokenHash = tokenHash;
            ExpiresAtUtc = expiresAtUtc;
            IpAddress = ipAddress;
            Status = SessionStatus.Active;
            Touch();
        }

        public void Revoke()
        {
            Status = SessionStatus.Revoked;
            RevokedAtUtc = DateTimeOffset.UtcNow;
            Touch();
        }
        #endregion

    }
}
