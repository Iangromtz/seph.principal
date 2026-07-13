using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.CreateMapInstitucionPeriodo
{
    public sealed record CreateMapInstitucionPeriodoCommand(
     long IdInstitucion,
     long IdPeriodo,
     bool BitCapturaAbierta,
     DateTime? DateFechaApertura,
     DateTime? DateFechaCierre,
     Guid IdUsuarioRegistro)
     : IRequest<ResponseWrapper<MapInstitucionPeriodoDto>>
    {
    }
}
