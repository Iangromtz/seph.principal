namespace Seph.Principal.Application.Features.Users.DTOs
{
    /// <summary>
    /// Forma cruda de un ApplicationUser expuesta por IIdentityService hacia la
    /// capa de Application, sin depender de Infraestructure.Identity.
    /// </summary>
    public sealed record AdminUserRawDto(
        Guid Id,
        string FullName,
        string Email,
        long? IdInstitucion,
        string? StrRFC,
        string? StrSNII,
        long? IdCatNivelAcademico,
        string? StrRutaIne,
        string? StrRutaFotografia,
        bool IsActive);
}
