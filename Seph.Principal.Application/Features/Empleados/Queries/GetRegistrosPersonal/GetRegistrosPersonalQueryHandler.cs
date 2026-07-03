using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.Empleados.Queries.GetRegistrosPersonal
{
    /// <summary>
    /// Arma el concentrado: empleados capturados por el usuario + su historial de contrato,
    /// resolviendo los textos de los catálogos (sexo, institución, tipo personal, tipo contrato, área).
    /// </summary>
    public sealed class GetRegistrosPersonalQueryHandler(
        IEmpleadosRepository empleadosRepository,
        IHistorialContratoRepository historialContratoRepository,
        ICatSexoRepository catSexoRepository,
        ICatTipoPersonalRepository catTipoPersonalRepository,
        ICatTipoContratoRepository catTipoContratoRepository,
        ICatAreaRepository catAreaRepository,
        IInstitucionRepository institucionRepository)
        : IRequestHandler<GetRegistrosPersonalQuery, ResponseWrapper<IReadOnlyList<RegistroPersonalDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<RegistroPersonalDto>>> Handle(
            GetRegistrosPersonalQuery request, CancellationToken cancellationToken)
        {
            var empleados = await empleadosRepository.GetAllAsync(cancellationToken);
            var historiales = await historialContratoRepository.GetAllAsync(cancellationToken);
            var sexos = await catSexoRepository.GetAllAsync(cancellationToken);
            var tiposPersonal = await catTipoPersonalRepository.GetAllAsync(cancellationToken);
            var tiposContrato = await catTipoContratoRepository.GetAllAsync(cancellationToken);
            var areas = await catAreaRepository.GetAllAsync(cancellationToken);
            var instituciones = await institucionRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<RegistroPersonalDto> response = empleados
                .Where(empleado => empleado.IdUsuarioRegistro == request.IdUsuarioRegistro)
                .OrderByDescending(empleado => empleado.DateTimeFechaRegistro)
                .Select(empleado =>
                {
                    // Contrato más reciente del empleado (null si el registro quedó incompleto).
                    var contrato = historiales
                        .Where(historial => historial.IdEmpleado == empleado.Id)
                        .OrderByDescending(historial => historial.DateTimeFechaRegistro)
                        .FirstOrDefault();

                    return new RegistroPersonalDto(
                        empleado.Id,
                        empleado.StrNombre ?? string.Empty,
                        empleado.StrApellidoPat ?? string.Empty,
                        empleado.StrApellidoMat ?? string.Empty,
                        empleado.StrCurp ?? string.Empty,
                        sexos.FirstOrDefault(sexo => sexo.Id == empleado.IdSexo)?.StrValor ?? string.Empty,
                        empleado.DateTimeFechaRegistro,
                        empleado.BitActivo,
                        contrato is not null,
                        contrato is null ? null : instituciones.FirstOrDefault(i => i.Id == contrato.IdInstitucion)?.StrNombre,
                        contrato is null ? null : tiposPersonal.FirstOrDefault(t => t.Id == contrato.IdTipoPersonal)?.StrValor,
                        contrato is null ? null : tiposContrato.FirstOrDefault(t => t.Id == contrato.IdTipoContrato)?.StrValor,
                        contrato is null ? null : areas.FirstOrDefault(a => a.Id == contrato.IdArea)?.StrValor,
                        contrato?.DateFechaIngreso);
                })
                .ToList();

            return ResponseFactory.Success(
                response,
                "Registros de personal obtenidos correctamente");
        }
    }
}
