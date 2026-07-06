using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;

namespace Seph.Principal.Application.Features.Empleados.Queries.GetEmpleadoById
{
    public sealed record GetEmpleadoByIdQuery(long IdEmpleado)
     : IRequest<ResponseWrapper<EmpleadoDetailDto>>;
}
