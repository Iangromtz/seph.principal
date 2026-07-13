using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteMatricula.DTOs
{
    public sealed record ReporteMatriculaComparativoDto(
      string PeriodoActual,
      int MatriculaActual,
      string? PeriodoAnterior,
      int? MatriculaAnterior,
      int Diferencia,
      decimal Porcentaje,
      string Estado);
}
