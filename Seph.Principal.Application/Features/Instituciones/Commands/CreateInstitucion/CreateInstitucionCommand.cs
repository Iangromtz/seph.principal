using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion
{
    public sealed record CreateInstitucionCommand(
        string StrNombre,
        string StrSiglas,
        string StrCct,
        string StrDireccion,
        DateTime? DateFechaCreacion,
        string? StrDecretoCreacion,
        string? StrSitioWeb,
        string StrCorreoInstitucional,
        string? StrTelefonoInstitucional,
        long IdMunicipio)
        : IRequest<ResponseWrapper<InstitucionDto>>;
}
