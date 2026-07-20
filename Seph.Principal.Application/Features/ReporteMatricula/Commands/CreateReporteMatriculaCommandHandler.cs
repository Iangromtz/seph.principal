using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteMatricula.Commands
{
    public sealed class CreateReporteMatriculaCommandHandler(
         IReporteMatriculaRepository reporteMatriculaRepository,
         IUnitOfWork unitOfWork,
         IBitacoraService bitacoraService,
         ICurrentUserService currentUserService)
         : IRequestHandler<CreateReporteMatriculaCommand, ResponseWrapper<ReporteMatriculaDto>>
    {
        public async Task<ResponseWrapper<ReporteMatriculaDto>> Handle(
            CreateReporteMatriculaCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos reportes para el mismo periodo institucional.
            var exists = await reporteMatriculaRepository.ExistsByMapInstitucionPeriodoAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<ReporteMatriculaDto>(
                    "Ya existe un reporte de matrícula registrado para este periodo.",
                    HttpStatusCode.BadRequest);
            }
            var reporteMatricula = new Domain.Entities.ReporteMatricula
            {
                IdMapInstitucionPeriodo = request.IdMapInstitucionPeriodo,
                IntTotal = request.IntTotal,
                IntTotalHombres = request.IntTotalHombres,
                IntTotalMujeres = request.IntTotalMujeres,
                IntTsu = request.IntTsu,
                IntLicenciatura = request.IntLicenciatura,
                IntPostgrado = request.IntPostgrado,
                DecimalTazaDesercion = request.DecimalTazaDesercion,
                DecimalTazaReprobacion = request.DecimalTazaReprobacion,
                DecimalTazaEficienciaTerminal = request.DecimalTazaEficienciaTerminal,
                DateTimeFechaRegistro = DateTime.Now,
                IdUsuarioRegistro = request.IdUsuarioRegistro,
                BitActivo = true
            };

            await reporteMatriculaRepository.AddAsync(
                reporteMatricula,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await bitacoraService.RegistrarAsync(
    "Matricula",
    reporteMatricula.Id.ToString(),
    "Agregar",
    currentUserService.UserId?.ToString() ?? "desconocido",
    currentUserService.Email?.ToString() ?? "desconocido",
    reporteMatricula,
    cancellationToken);

            var dto = new ReporteMatriculaDto(
                reporteMatricula.Id,
                reporteMatricula.IdMapInstitucionPeriodo,
                reporteMatricula.IntTotal,
                reporteMatricula.IntTotalHombres,
                reporteMatricula.IntTotalMujeres,
                reporteMatricula.IntTsu,
                reporteMatricula.IntLicenciatura,
                reporteMatricula.IntPostgrado,
                reporteMatricula.DecimalTazaDesercion,
                reporteMatricula.DecimalTazaReprobacion,
                reporteMatricula.DecimalTazaEficienciaTerminal,
                reporteMatricula.DateTimeFechaRegistro,
                reporteMatricula.IdUsuarioRegistro,
                reporteMatricula.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de matrícula registrado correctamente");
        }
    }
}
