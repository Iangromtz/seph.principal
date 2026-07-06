using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Commands.UpdateInstitucion
{
    public sealed record UpdateInstitucionCommand(
        long Id,
        string StrNombre,
        string? StrSiglas,
        string? StrCct,
        string? StrDireccion,
        DateTime? DateFechaCreacion,
        string? StrDecretoCreacion,
        string? StrSitioWeb,
        string? StrCorreoInstitucional,
        string? StrTelefonoInstitucional,
        long IdMunicipio)
        : IRequest<ResponseWrapper<InstitucionDto>>;
}
