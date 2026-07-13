using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;

namespace Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaEstadisticas
{
    public sealed record GetReporteMatriculaEstadisticasQuery(long IdMapInstitucionPeriodo)
         : IRequest<ResponseWrapper<ReporteMatriculaEstadisticasDto>>;
}
