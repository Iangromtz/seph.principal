using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalEstadisticas
{
    public sealed record GetReportePersonalEstadisticasQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReportePersonalEstadisticasDto>>;
}