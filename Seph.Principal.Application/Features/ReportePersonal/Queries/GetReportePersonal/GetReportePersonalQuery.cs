using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonal
{
    public sealed record GetReportePersonalQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReportePersonalDto>>;
}