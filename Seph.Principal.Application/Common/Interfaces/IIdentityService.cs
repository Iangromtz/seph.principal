using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticatedUserDto> ValidateCredentialAsync(string email, string password, CancellationToken cancellationToken);
        Task<AuthenticatedUserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);
        Task<Guid?> RegisterAsync(string fullName, string email, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Crea un usuario con un rol específico y una institución asignada.
        /// Usado por el SuperAdmin para crear Admins y por los Admins para crear usuarios de su institución.
        /// Devuelve el Id del usuario creado, o null si el correo ya existe o falla la creación.
        /// </summary>
        Task<Guid?> CreateUserWithRoleAsync(
            string fullName, string email, string password, string role, long? idInstitucion,
            string? rutaIne, string? rutaFotografia, string? rfc, string? snii, long? idNivelAcademico,
            CancellationToken cancellationToken);
        Task MarkLastLoginAsync(Guid userId, CancellationToken cancellationToken);
        Task<AuthenticatedUserDto> FindOrCreateGoogleUserAsync(string email, string fullName, string googleId, CancellationToken cancellationToken);
        /// <summary>
        /// Metodo para obtener el id del usuario a partir de su correo electrónico,
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid?> GetUserIdByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Metodo para verificar si el correo electrónico de un usuario específico ha sido confirmado,
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsEmailConfirmedAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Metodo para marcar el correo electrónico de un usuario específico como confirmado,
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Indica si una institución tiene usuarios activos (Admin/Usuario) asignados.
        /// Se usa para impedir que se desactive una institución que sigue en uso.
        /// </summary>
        Task<bool> HasActiveUsersInInstitutionAsync(long idInstitucion, CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene todos los usuarios que tienen un rol específico (ej. "Admin" = Enlaces Académicos).
        /// </summary>
        Task<IReadOnlyList<AdminUserRawDto>> GetUsersByRoleAsync(string role, CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene un usuario por id en su forma cruda (para detalle/edición de Enlaces Académicos).
        /// </summary>
        Task<AdminUserRawDto?> GetUserByIdRawAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Activa o desactiva un usuario (IsActive). Devuelve false si el usuario no existe.
        /// </summary>
        Task<bool> SetUserActiveStatusAsync(Guid userId, bool isActive, CancellationToken cancellationToken);

        /// <summary>
        /// Actualiza los datos editables de un Enlace Académico. Devuelve false si el usuario no existe.
        /// </summary>
        Task<bool> UpdateUserDetailsAsync(
            Guid userId, string fullName, long? idInstitucion,
            string? rutaIne, string? rutaFotografia, string? rfc, string? snii, long? idNivelAcademico,
            CancellationToken cancellationToken);
    }
}
