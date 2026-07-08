namespace Seph.Principal.Application.Features.Users.DTOs
{
    /// <summary>
    /// Cuerpo de la petición para que el SuperAdmin cree un Admin de institución.
    /// </summary>
    public sealed record CreateAdminRequest(
     string FullName,
     string Email,
     string Password,
     long IdInstitucion,
     string? StrRutaIne,
     string? StrRutaFotografia,
     string? StrRFC,
     string? StrSNII,
     long? IdNivelAcademico,
     IReadOnlyList<long> IdsPerfilAcademico);
}
