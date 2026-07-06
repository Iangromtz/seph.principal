using MediatR;
using Seph.Principal.Application.Common.Models;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.Empleados.Commands.UpdateDatosAcademicos
{
    /// <summary>
    /// Actualiza el SNI/SNII y los perfiles académicos (programas educativos) de un
    /// empleado ya registrado. Es el paso "Perfil académico" del Registro de Personal,
    /// posterior a la captura de Información Personal.
    /// </summary>
    public sealed record UpdateDatosAcademicosEmpleadoCommand(
        long IdEmpleado,
        string? StrSNII,
        IReadOnlyList<long> IdsPerfilAcademico)
        : IRequest<ResponseWrapper<string>>;
}
