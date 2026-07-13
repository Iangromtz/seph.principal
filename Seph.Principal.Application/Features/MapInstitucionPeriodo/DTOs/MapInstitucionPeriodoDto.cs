using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs
{
    public sealed record MapInstitucionPeriodoDto(
         long Id,
         long IdInstitucion,
         long IdPeriodo,
         bool BitCapturaAbierta,
         DateTime? DateFechaApertura,
         DateTime? DateFechaCierre,
         DateTime DateTimeFechaRegistro,
         Guid IdUsuarioRegistro,
         bool BitActivo);
}
