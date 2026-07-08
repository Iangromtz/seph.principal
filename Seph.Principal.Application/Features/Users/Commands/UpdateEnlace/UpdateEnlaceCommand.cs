using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Users.Commands.UpdateEnlace
{
    public sealed record UpdateEnlaceCommand(
        Guid Id,
        string FullName,
        long IdInstitucion,
        string? StrRutaIne,
        string? StrRutaFotografia,
        string? StrRFC,
        string? StrSNII,
        long? IdNivelAcademico,
        IReadOnlyList<long> IdsPerfilAcademico)
        : IRequest<ResponseWrapper<string>>;
}
