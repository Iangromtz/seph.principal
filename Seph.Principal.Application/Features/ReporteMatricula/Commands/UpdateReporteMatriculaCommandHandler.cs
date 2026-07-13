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
    public sealed class UpdateReporteMatriculaCommandHandler(
        IReporteMatriculaRepository reporteMatriculaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<
            UpdateReporteMatriculaCommand,
            ResponseWrapper<ReporteMatriculaDto>>
    {
        public async Task<ResponseWrapper<ReporteMatriculaDto>> Handle(
            UpdateReporteMatriculaCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte del periodo institucional.
            var reporte = await reporteMatriculaRepository
                .GetByMapInstitucionPeriodoAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaDto>(
                    "No existe un reporte de matrícula para actualizar.",
                    HttpStatusCode.NotFound);
            }

            // Actualiza únicamente la información capturable.
            reporte.IntTotal = request.IntTotal;
            reporte.IntTotalHombres = request.IntTotalHombres;
            reporte.IntTotalMujeres = request.IntTotalMujeres;
            reporte.IntTsu = request.IntTsu;
            reporte.IntLicenciatura = request.IntLicenciatura;
            reporte.IntPostgrado = request.IntPostgrado;
            reporte.DecimalTazaDesercion = request.DecimalTazaDesercion;
            reporte.DecimalTazaReprobacion = request.DecimalTazaReprobacion;
            reporte.DecimalTazaEficienciaTerminal =
                request.DecimalTazaEficienciaTerminal;

            reporteMatriculaRepository.Update(reporte);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new ReporteMatriculaDto(
                reporte.Id,
                reporte.IdMapInstitucionPeriodo,
                reporte.IntTotal,
                reporte.IntTotalHombres,
                reporte.IntTotalMujeres,
                reporte.IntTsu,
                reporte.IntLicenciatura,
                reporte.IntPostgrado,
                reporte.DecimalTazaDesercion,
                reporte.DecimalTazaReprobacion,
                reporte.DecimalTazaEficienciaTerminal,
                reporte.DateTimeFechaRegistro,
                reporte.IdUsuarioRegistro,
                reporte.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de matrícula actualizado correctamente");
        }
    }
}
