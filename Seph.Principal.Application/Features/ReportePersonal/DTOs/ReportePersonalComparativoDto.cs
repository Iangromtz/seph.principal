using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReportePersonal.DTOs
{
    public sealed record ReportePersonalComparativoDto(
        string PeriodoActual,
        int TotalPersonalActual,
        string? PeriodoAnterior,
        int? TotalPersonalAnterior,
        int Diferencia,
        decimal Porcentaje,
        string Estado);
}