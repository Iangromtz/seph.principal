using System;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalEstadisticas
{
    public sealed class GetReportePersonalEstadisticasQueryHandler(
        IReportePersonalRepository reportePersonalRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository,
        ICatNivelAcademicoRepository catNivelAcademicoRepository)
        : IRequestHandler<GetReportePersonalEstadisticasQuery,
            ResponseWrapper<ReportePersonalEstadisticasDto>>
    {
        public async Task<ResponseWrapper<ReportePersonalEstadisticasDto>> Handle(
            GetReportePersonalEstadisticasQuery request,
            CancellationToken cancellationToken)
        {
            // Obtiene el reporte capturado para el periodo institucional.
            var reporte = await reportePersonalRepository.GetByMapInstitucionPeriodoAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReportePersonalEstadisticasDto>(
                    "No existe un reporte de personal para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo.
            var mapInstitucionPeriodo = await mapInstitucionPeriodoRepository.GetByIdAsync(
                request.IdMapInstitucionPeriodo,
                cancellationToken);

            if (mapInstitucionPeriodo is null)
            {
                return ResponseFactory.Failure<ReportePersonalEstadisticasDto>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene el periodo.
            var periodo = await catPeriodoRepository.GetByIdAsync(
                mapInstitucionPeriodo.IdPeriodo,
                cancellationToken);

            if (periodo is null)
            {
                return ResponseFactory.Failure<ReportePersonalEstadisticasDto>(
                    "No existe el periodo seleccionado.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene el nivel académico predominante.
            var nivelAcademico = await catNivelAcademicoRepository.GetByIdAsync(
                reporte.IdNivelAcademico,
                cancellationToken);

            var total = reporte.IntTotalGeneral;

            var porcentajeDirectivos = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDirectivos / total * 100, 2);

            var porcentajeAdministrativos = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalAdministrativos / total * 100, 2);

            var porcentajeDocentes = total == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDocentes / total * 100, 2);

            var porcentajeDirectivosHombres = reporte.IntTotalDirectivos == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDirectivosHombres / reporte.IntTotalDirectivos * 100, 2);

            var porcentajeDirectivosMujeres = reporte.IntTotalDirectivos == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDirectivosMujeres / reporte.IntTotalDirectivos * 100, 2);

            var porcentajeAdministrativosHombres = reporte.IntTotalAdministrativos == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalAdministrativosHombres / reporte.IntTotalAdministrativos * 100, 2);

            var porcentajeAdministrativosMujeres = reporte.IntTotalAdministrativos == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalAdministrativosMujeres / reporte.IntTotalAdministrativos * 100, 2);

            var porcentajeDocentesHombres = reporte.IntTotalDocentes == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDocentesHombres / reporte.IntTotalDocentes * 100, 2);

            var porcentajeDocentesMujeres = reporte.IntTotalDocentes == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDocentesMujeres / reporte.IntTotalDocentes * 100, 2);

            var porcentajeTiempoCompleto = reporte.IntTotalDocentes == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDocentesTiempoCompleto / reporte.IntTotalDocentes * 100, 2);

            var porcentajeAsignatura = reporte.IntTotalDocentes == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDocentesAsignatura / reporte.IntTotalDocentes * 100, 2);

            var porcentajeHora = reporte.IntTotalDocentes == 0
                ? 0
                : Math.Round((decimal)reporte.IntTotalDocentesHora / reporte.IntTotalDocentes * 100, 2);

            var dto = new ReportePersonalEstadisticasDto(
                periodo.StrValor,
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
                nivelAcademico?.StrValor ?? "No definido",
                porcentajeDirectivos,
                porcentajeAdministrativos,
                porcentajeDocentes,
                porcentajeDirectivosHombres,
                porcentajeDirectivosMujeres,
                porcentajeAdministrativosHombres,
                porcentajeAdministrativosMujeres,
                porcentajeDocentesHombres,
                porcentajeDocentesMujeres,
                porcentajeTiempoCompleto,
                porcentajeAsignatura,
                porcentajeHora);

            return ResponseFactory.Success(
                dto,
                "Estadísticas de personal obtenidas correctamente");
        }
    }
}