using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using System;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Commands
{
    /// <summary>
    /// Comando para registrar un reporte de vinculación.
    /// </summary>
    public sealed record CreateReporteVinculacionCommand(
        long IdMapInstitucionPeriodo,
        int IntTotalConveniosActivos,
        bool BitPracticasProfesionales,
        bool BitServicioSocial,
        bool BitSeguimientoEgresados,
        long? IdMecanismoSeguimiento,
        decimal DecimalPorcentajeLaborando,
        Guid IdUsuarioRegistro,
        List<SectorVinculadoDto> SectoresVinculados)
        : IRequest<ResponseWrapper<ReporteVinculacionDto>>;
}