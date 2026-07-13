using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteMatricula.DTOs
{
    public sealed record ReporteMatriculaDto(
        long Id,
        long IdMapInstitucionPeriodo,
        int IntTotal,
        int IntTotalHombres,
        int IntTotalMujeres,
        int IntTsu,
        int IntLicenciatura,
        int IntPostgrado,
        decimal DecimalTazaDesercion,
        decimal DecimalTazaReprobacion,
        decimal DecimalTazaEficienciaTerminal,
        DateTime DateTimeFechaRegistro,
        Guid IdUsuarioRegistro,
        bool BitActivo);
}
