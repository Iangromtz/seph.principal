using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePersonal.Commands
{
    public sealed class UpdateReportePersonalCommandHandler(
        IReportePersonalRepository reportePersonalRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<
            UpdateReportePersonalCommand,
            ResponseWrapper<ReportePersonalDto>>
    {
        public async Task<ResponseWrapper<ReportePersonalDto>> Handle(
            UpdateReportePersonalCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte del periodo institucional.
            var reporte = await reportePersonalRepository
              .GetByMapInstitucionPeriodoForUpdateAsync(
                 request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReportePersonalDto>(
                    "No existe un reporte de personal para actualizar.",
                    HttpStatusCode.NotFound);
            }

            // Actualiza únicamente la información capturable.
            reporte.IntTotalGeneral = request.IntTotalGeneral;
            reporte.IntTotalDirectivos = request.IntTotalDirectivos;
            reporte.IntTotalDirectivosHombres =
                request.IntTotalDirectivosHombres;
            reporte.IntTotalDirectivosMujeres =
                request.IntTotalDirectivosMujeres;
            reporte.IntTotalAdministrativos =
                request.IntTotalAdministrativos;
            reporte.IntTotalAdministrativosHombres =
                request.IntTotalAdministrativosHombres;
            reporte.IntTotalAdministrativosMujeres =
                request.IntTotalAdministrativosMujeres;
            reporte.IntTotalDocentes = request.IntTotalDocentes;
            reporte.IntTotalDocentesHombres =
                request.IntTotalDocentesHombres;
            reporte.IntTotalDocentesMujeres =
                request.IntTotalDocentesMujeres;
            reporte.IntTotalDocentesTiempoCompleto =
                request.IntTotalDocentesTiempoCompleto;
            reporte.IntTotalDocentesAsignatura =
                request.IntTotalDocentesAsignatura;
            reporte.IntTotalDocentesHora =
                request.IntTotalDocentesHora;
            reporte.IdNivelAcademico =
                request.IdNivelAcademico;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new ReportePersonalDto(
                reporte.Id,
                reporte.IdMapInstitucionPeriodo,
                reporte.IntTotalGeneral,
                reporte.IntTotalDirectivos,
                reporte.IntTotalDirectivosHombres,
                reporte.IntTotalDirectivosMujeres,
                reporte.IntTotalAdministrativos,
                reporte.IntTotalAdministrativosHombres,
                reporte.IntTotalAdministrativosMujeres,
                reporte.IntTotalDocentes,
                reporte.IntTotalDocentesHombres,
                reporte.IntTotalDocentesMujeres,
                reporte.IntTotalDocentesTiempoCompleto,
                reporte.IntTotalDocentesAsignatura,
                reporte.IntTotalDocentesHora,
                reporte.IdNivelAcademico,
                reporte.DateTimeFechaRegistro,
                reporte.IdUsuarioRegistro,
                reporte.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de personal actualizado correctamente");
        }
    }
}