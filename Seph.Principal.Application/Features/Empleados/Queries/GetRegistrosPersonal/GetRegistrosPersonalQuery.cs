using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;

namespace Seph.Principal.Application.Features.Empleados.Queries.GetRegistrosPersonal
{
    /// <summary>
    /// Obtiene el concentrado de registros de personal capturados por el usuario autenticado.
    /// </summary>
    public sealed record GetRegistrosPersonalQuery(Guid IdUsuarioRegistro)
        : IRequest<ResponseWrapper<IReadOnlyList<RegistroPersonalDto>>>;
}
