using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteMatricula.DTOs
{
    public sealed record ReporteMatriculaEstadisticasDto(
            string Periodo,
            int MatriculaTotal,
            int Hombres,
            int Mujeres,
            int Tsu,
            int Licenciatura,
            int Postgrado,
            decimal TasaDesercion,
            decimal TasaReprobacion,
            decimal TasaEficienciaTerminal,
            decimal PorcentajeHombres,
            decimal PorcentajeMujeres,
            decimal PorcentajeTsu,
            decimal PorcentajeLicenciatura,
            decimal PorcentajePostgrado);
}
