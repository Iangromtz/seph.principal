namespace Seph.Principal.Application.Features.Users.DTOs
{
    public sealed record UpdateEnlaceRequest(
        string FullName,
        long IdInstitucion,
        string? StrRutaIne,
        string? StrRutaFotografia,
        string? StrRFC,
        string? StrSNII,
        long? IdNivelAcademico,
        IReadOnlyList<long> IdsPerfilAcademico);
}
