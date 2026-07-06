using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Empleados.Commands.UpdateDatosAcademicos
{
    public sealed class UpdateDatosAcademicosEmpleadoCommandHandler(
        IEmpleadosRepository empleadosRepository,
        IMapEmpleadoPerfilAcademicoRepository mapEmpleadoPerfilAcademicoRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateDatosAcademicosEmpleadoCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(
            UpdateDatosAcademicosEmpleadoCommand request,
            CancellationToken cancellationToken)
        {
            var empleado = await empleadosRepository.GetByIdAsync(request.IdEmpleado, cancellationToken);

            if (empleado is null)
            {
                return ResponseFactory.Failure<string>("Empleado no encontrado", HttpStatusCode.NotFound);
            }

            empleado.StrSNII = request.StrSNII;
            empleado.BitDatosAcademicosCompletos = true;
            empleadosRepository.Update(empleado);

            // Se eliminan los perfiles académicos previos para que la operación
            // sea idempotente (por si el usuario regresa y vuelve a guardar este paso).
            await mapEmpleadoPerfilAcademicoRepository.DeleteByEmpleadoIdAsync(request.IdEmpleado, cancellationToken);

            foreach (var idPerfilAcademico in request.IdsPerfilAcademico.Distinct())
            {
                await mapEmpleadoPerfilAcademicoRepository.AddAsync(
                    new MapEmpleadoPerfilAcademico(request.IdEmpleado, idPerfilAcademico),
                    cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success(
                "Datos académicos actualizados correctamente",
                "Datos académicos actualizados correctamente");
        }
    }
}
