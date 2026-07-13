using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaEstadisticas
{
    public sealed class GetReporteMatriculaEstadisticasQueryHandler(
         IReporteMatriculaRepository reporteMatriculaRepository,
         IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
         ICatPeriodoRepository catPeriodoRepository)
         : IRequestHandler<GetReporteMatriculaEstadisticasQuery, ResponseWrapper<ReporteMatriculaEstadisticasDto>>
    {
        public async Task<ResponseWrapper<ReporteMatriculaEstadisticasDto>> Handle(
            GetReporteMatriculaEstadisticasQuery request,
            CancellationToken cancellationToken)
        {
            // Obtiene el reporte capturado para el periodo institucional.
            var reporte = await reporteMatriculaRepository.GetByMapInstitucionPeriodoAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaEstadisticasDto>(
                    "No existe un reporte de matrícula para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo.
            var mapInstitucionPeriodo = await mapInstitucionPeriodoRepository.GetByIdAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (mapInstitucionPeriodo is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaEstadisticasDto>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene el nombre del periodo para mostrarlo en el frontend.
            var periodo = await catPeriodoRepository.GetByIdAsync(
                mapInstitucionPeriodo.IdPeriodo,
                cancellationToken);

            if (periodo is null)
            {
                return ResponseFactory.Failure<ReporteMatriculaEstadisticasDto>(
                    "No existe el periodo seleccionado.",
                    HttpStatusCode.NotFound);
            }

            var total = reporte.IntTotal;

            var porcentajeHombres = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalHombres / total * 100, 2);

            var porcentajeMujeres = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalMujeres / total * 100, 2);

            var porcentajeTsu = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntTsu / total * 100, 2);

            var porcentajeLicenciatura = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntLicenciatura / total * 100, 2);

            var porcentajePostgrado = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntPostgrado / total * 100, 2);

            var dto = new ReporteMatriculaEstadisticasDto(
                periodo.StrValor,
                reporte.IntTotal,
                reporte.IntTotalHombres,
                reporte.IntTotalMujeres,
                reporte.IntTsu,
                reporte.IntLicenciatura,
                reporte.IntPostgrado,
                reporte.DecimalTazaDesercion,
                reporte.DecimalTazaReprobacion,
                reporte.DecimalTazaEficienciaTerminal,
                porcentajeHombres,
                porcentajeMujeres,
                porcentajeTsu,
                porcentajeLicenciatura,
                porcentajePostgrado);

            return ResponseFactory.Success(
                dto,
                "Estadísticas de matrícula obtenidas correctamente");
        }
    }
}
