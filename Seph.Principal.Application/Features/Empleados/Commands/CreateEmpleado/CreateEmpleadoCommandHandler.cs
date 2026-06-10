using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado
{
    public sealed class CreateEmpleadoCommandHandler(IEmpleadosRepository empleadosRepository,
     IUnitOfWork unitOfWork): IRequestHandler<CreateEmpleadoCommand, ResponseWrapper<EmpleadosDto>>
    {
        public async Task<ResponseWrapper<EmpleadosDto>> Handle(CreateEmpleadoCommand request, CancellationToken cancellationToken)
        {
            var empleado = new Seph.Principal.Domain.Entities.Empleado
            {
                StrNombre = request.StrNombre,
                StrApellidoPat = request.StrApellidoPat,
                StrApellidoMat = request.StrApellidoMat,
                StrCurp = request.StrCurp,
                IdSexo = request.IdSexo,
                DateTimeFechaRegistro = request.DateTimeFechaRegistro,
                IdUsuarioRegistro = request.IdUsuarioRegistro,
                BitActivo = request.BitActivo,
                DateTimeFechaBaja = request.DateTimeFechaBaja,
            };



            await empleadosRepository.AddAsync(empleado, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new EmpleadosDto(empleado.StrNombre, empleado.StrApellidoPat, empleado.StrApellidoMat, empleado.StrCurp, empleado.IdSexo, empleado.DateTimeFechaRegistro, empleado.IdUsuarioRegistro, empleado.BitActivo, empleado.DateTimeFechaBaja);

            return ResponseFactory.Success(
                dto,
                "Empleado registrado correctamente");
        }
    }
}
