namespace Seph.Principal.Application.Features.Users.DTOs
{
    public sealed record EnlaceAcademicoDto(
        Guid Id,
        string FullName,
        string Email,
        long IdInstitucion,
        string StrInstitucion,
        string? StrRFC,
        string? StrSNII,
        long? IdNivelAcademico,
        string? StrNivelAcademico,
        string? StrRutaIne,
        string? StrRutaFotografia,
        bool IsActive,
        IReadOnlyList<long> IdsPerfilAcademico);
}
