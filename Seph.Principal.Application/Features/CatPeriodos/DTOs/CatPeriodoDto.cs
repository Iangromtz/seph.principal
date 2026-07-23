using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPeriodos.DTOs
{
    public sealed record CatPeriodoDto(
        long Id,
        string StrNombre,
        string StrDescripcion,
        int IntAnio,
        int IntNumeroPeriodo,
        DateTime DateFechaInicio,
        DateTime DateFechaFin,
        bool BitActivo,
        long IdTipoPeriodo,
        string StrTipoPeriodo);
}