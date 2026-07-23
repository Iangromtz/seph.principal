using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Commands
{
    /*
     * Actualiza el reporte de vinculación asociado
     * a una relación institución-periodo.
     */
    public sealed record UpdateReporteVinculacionCommand(
        long IdMapInstitucionPeriodo,
        int IntTotalConveniosActivos,
        bool BitPracticasProfesionales,
        bool BitServicioSocial,
        bool BitSeguimientoEgresados,
        long? IdMecanismoSeguimiento,
        decimal DecimalPorcentajeLaborando,
        List<SectorVinculadoDto> SectoresVinculados)
        : IRequest<ResponseWrapper<ReporteVinculacionDto>>;
}