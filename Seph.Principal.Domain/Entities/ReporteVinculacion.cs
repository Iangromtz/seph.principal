using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa la información de vinculación y resultados
    /// registrada por una institución en un periodo.
    /// </summary>
    public class ReporteVinculacion
    {
        /// <summary>
        /// Identificador único del reporte.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador de la relación entre
        /// la institución y el periodo activo.
        /// </summary>
        public long IdMapInstitucionPeriodo { get; set; }

        /// <summary>
        /// Total de convenios activos de la institución.
        /// </summary>
        public int IntTotalConveniosActivos { get; set; }

        /// <summary>
        /// Indica si la institución cuenta
        /// con prácticas profesionales.
        /// </summary>
        public bool BitPracticasProfesionales { get; set; }

        /// <summary>
        /// Indica si la institución cuenta
        /// con servicio social.
        /// </summary>
        public bool BitServicioSocial { get; set; }

        /// <summary>
        /// Indica si la institución realiza
        /// seguimiento de egresados.
        /// </summary>
        public bool BitSeguimientoEgresados { get; set; }

        /// <summary>
        /// Porcentaje estimado de egresados
        /// que se encuentran laborando.
        /// </summary>
        public decimal DecimalPorcentajeLaborando { get; set; }

        /// <summary>
        /// Fecha en la que se registró la información.
        /// </summary>
        public DateTime DateTimeFechaRegistro { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el registro.
        /// </summary>
        public Guid IdUsuarioRegistro { get; set; }

        /// <summary>
        /// Indica si el registro se encuentra activo.
        /// </summary>
        public bool BitActivo { get; set; }

        /// <summary>
        /// Identificador del catálogo de mecanismos
        /// utilizados para el seguimiento de egresados.
        /// </summary>
        public long? IdMecanismoSeguimiento { get; set; }

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia vacía
        /// de la entidad ReporteVinculacion.
        /// </summary>
        public ReporteVinculacion()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa del reporte.
        /// </summary>
        public ReporteVinculacion(
            long id,
            long idMapInstitucionPeriodo,
            int intTotalConveniosActivos,
            bool bitPracticasProfesionales,
            bool bitServicioSocial,
            bool bitSeguimientoEgresados,
            decimal decimalPorcentajeLaborando,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo,
            long? idMecanismoSeguimiento)
        {
            Id = id;
            IdMapInstitucionPeriodo = idMapInstitucionPeriodo;
            IntTotalConveniosActivos = intTotalConveniosActivos;
            BitPracticasProfesionales = bitPracticasProfesionales;
            BitServicioSocial = bitServicioSocial;
            BitSeguimientoEgresados = bitSeguimientoEgresados;
            DecimalPorcentajeLaborando = decimalPorcentajeLaborando;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
            IdMecanismoSeguimiento = idMecanismoSeguimiento;
        }

        #endregion
    }
}