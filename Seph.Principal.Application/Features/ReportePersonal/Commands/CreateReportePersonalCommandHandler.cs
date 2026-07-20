using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePersonal.Commands
{
    public sealed class CreateReportePersonalCommandHandler(
        IReportePersonalRepository reportePersonalRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateReportePersonalCommand, ResponseWrapper<ReportePersonalDto>>
    {
        public async Task<ResponseWrapper<ReportePersonalDto>> Handle(
            CreateReportePersonalCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos reportes para el mismo periodo institucional.
            var exists = await reportePersonalRepository.ExistsByMapInstitucionPeriodoAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<ReportePersonalDto>(
                    "Ya existe un reporte de personal registrado para este periodo.",
                    HttpStatusCode.BadRequest);
            }

            var reportePersonal = new Domain.Entities.ReportePersonal
            {
                IdMapInstitucionPeriodo = request.IdMapInstitucionPeriodo,
                IntTotalGeneral = request.IntTotalGeneral,
                IntTotalDirectivos = request.IntTotalDirectivos,
                IntTotalDirectivosHombres = request.IntTotalDirectivosHombres,
                IntTotalDirectivosMujeres = request.IntTotalDirectivosMujeres,
                IntTotalAdministrativos = request.IntTotalAdministrativos,
                IntTotalAdministrativosHombres = request.IntTotalAdministrativosHombres,
                IntTotalAdministrativosMujeres = request.IntTotalAdministrativosMujeres,
                IntTotalDocentes = request.IntTotalDocentes,
                IntTotalDocentesHombres = request.IntTotalDocentesHombres,
                IntTotalDocentesMujeres = request.IntTotalDocentesMujeres,
                IntTotalDocentesTiempoCompleto = request.IntTotalDocentesTiempoCompleto,
                IntTotalDocentesAsignatura = request.IntTotalDocentesAsignatura,
                IntTotalDocentesHora = request.IntTotalDocentesHora,
                IdNivelAcademico = request.IdNivelAcademico,
                DateTimeFechaRegistro = DateTime.Now,
                IdUsuarioRegistro = request.IdUsuarioRegistro,
                BitActivo = true
            };

            await reportePersonalRepository.AddAsync(
                reportePersonal,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new ReportePersonalDto(
                reportePersonal.Id,
                reportePersonal.IdMapInstitucionPeriodo,
                reportePersonal.IntTotalGeneral,
                reportePersonal.IntTotalDirectivos,
                reportePersonal.IntTotalDirectivosHombres,
                reportePersonal.IntTotalDirectivosMujeres,
                reportePersonal.IntTotalAdministrativos,
                reportePersonal.IntTotalAdministrativosHombres,
                reportePersonal.IntTotalAdministrativosMujeres,
                reportePersonal.IntTotalDocentes,
                reportePersonal.IntTotalDocentesHombres,
                reportePersonal.IntTotalDocentesMujeres,
                reportePersonal.IntTotalDocentesTiempoCompleto,
                reportePersonal.IntTotalDocentesAsignatura,
                reportePersonal.IntTotalDocentesHora,
                reportePersonal.IdNivelAcademico,
                reportePersonal.DateTimeFechaRegistro,
                reportePersonal.IdUsuarioRegistro,
                reportePersonal.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de personal registrado correctamente");
        }
    }
}