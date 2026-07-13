using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;

namespace Seph.Principal.Application.Features.ReporteMatricula.Commands
{
    /*
     * Actualiza el reporte de matrícula asociado
     * a una relación institución-periodo.
     */
    public sealed record UpdateReporteMatriculaCommand(
        long IdMapInstitucionPeriodo,
        int IntTotal,
        int IntTotalHombres,
        int IntTotalMujeres,
        int IntTsu,
        int IntLicenciatura,
        int IntPostgrado,
        decimal DecimalTazaDesercion,
        decimal DecimalTazaReprobacion,
        decimal DecimalTazaEficienciaTerminal)
        : IRequest<ResponseWrapper<ReporteMatriculaDto>>;
}
