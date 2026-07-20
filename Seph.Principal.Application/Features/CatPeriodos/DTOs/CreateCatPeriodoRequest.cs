using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPeriodos.DTOs
{
    /// <summary>
    /// Datos enviados por el administrador para registrar un periodo.
    /// El nombre visible del periodo se genera automáticamente en el backend
    /// utilizando la fecha inicial y la fecha final.
    /// </summary>
    public sealed record CreateCatPeriodoRequest(
        int IntAnio,
        int IntNumeroPeriodo,
        DateTime DateFechaInicio,
        DateTime DateFechaFin);
}
