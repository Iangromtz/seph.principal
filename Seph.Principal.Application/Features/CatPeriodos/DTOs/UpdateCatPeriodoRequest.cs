using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPeriodos.DTOs
{
    /// <summary>
    /// Datos necesarios para actualizar un periodo existente.
    /// El nombre y la descripción se generan nuevamente
    /// en el backend con base en el año, número y fechas.
    /// </summary>
    public sealed record UpdateCatPeriodoRequest(
        int IntAnio,
        int IntNumeroPeriodo,
        DateTime DateFechaInicio,
        DateTime DateFechaFin);
}
