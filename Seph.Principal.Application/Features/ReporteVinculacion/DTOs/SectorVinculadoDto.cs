using System;

namespace Seph.Principal.Application.Features.ReporteVinculacion.DTOs
{
    /// <summary>
    /// Representa un sector vinculado
    /// asociado a un reporte de vinculación.
    /// </summary>
    public sealed class SectorVinculadoDto
    {
        /// <summary>
        /// Identificador del sector vinculado.
        /// </summary>
        public long IdSectorVinculado { get; set; }

        /// <summary>
        /// Especifica otro sector cuando aplica.
        /// </summary>
        public string? StrOtros { get; set; }
    }
}