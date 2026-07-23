using System;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReporteVinculacion.DTOs
{
    /// <summary>
    /// Representa la información de un
    /// reporte de vinculación.
    /// </summary>
    public sealed class ReporteVinculacionDto
    {
        /// <summary>
        /// Identificador del reporte.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador del mapa
        /// institución-periodo.
        /// </summary>
        public long IdMapInstitucionPeriodo { get; set; }

        /// <summary>
        /// Total de convenios activos.
        /// </summary>
        public int IntTotalConveniosActivos { get; set; }

        /// <summary>
        /// Indica si la institución
        /// cuenta con prácticas profesionales.
        /// </summary>
        public bool BitPracticasProfesionales { get; set; }

        /// <summary>
        /// Indica si la institución
        /// cuenta con servicio social.
        /// </summary>
        public bool BitServicioSocial { get; set; }

        /// <summary>
        /// Indica si la institución realiza
        /// seguimiento de egresados.
        /// </summary>
        public bool BitSeguimientoEgresados { get; set; }

        /// <summary>
        /// Identificador del mecanismo
        /// de seguimiento.
        /// </summary>
        public long? IdMecanismoSeguimiento { get; set; }

        /// <summary>
        /// Porcentaje estimado de egresados
        /// que se encuentran laborando.
        /// </summary>
        public decimal DecimalPorcentajeLaborando { get; set; }

        /// <summary>
        /// Sectores con los que la institución
        /// mantiene vinculación.
        /// </summary>
        public List<SectorVinculadoDto> SectoresVinculados { get; set; }
            = new();
    }
}