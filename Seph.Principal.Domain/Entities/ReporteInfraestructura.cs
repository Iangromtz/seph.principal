using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa la información de infraestructura
    /// registrada por una institución en un periodo.
    /// </summary>
    public class ReporteInfraestructura
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
        /// Total de aulas disponibles.
        /// </summary>
        public int IntTotalAulas { get; set; }

        /// <summary>
        /// Total de laboratorios disponibles.
        /// </summary>
        public int IntTotalLaboratorios { get; set; }

        /// <summary>
        /// Total de talleres disponibles.
        /// </summary>
        public int IntTotalTalleres { get; set; }

        /// <summary>
        /// Indica si la institución cuenta con biblioteca.
        /// </summary>
        public bool BitBiblioteca { get; set; }

        /// <summary>
        /// Total de bibliotecas disponibles.
        /// </summary>
        public int IntTotalBibliotecas { get; set; }

        /// <summary>
        /// Total de equipos de cómputo disponibles.
        /// </summary>
        public int IntTotalComputo { get; set; }

        /// <summary>
        /// Identificador del catálogo de acceso a internet.
        /// </summary>
        public long IdInternet { get; set; }

        /// <summary>
        /// Identificador del catálogo de instalaciones
        /// adaptadas para personas con discapacidad.
        /// </summary>
        public long IdDiscapacitado { get; set; }

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

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia vacía
        /// de la entidad ReporteInfraestructura.
        /// </summary>
        public ReporteInfraestructura()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa del reporte.
        /// </summary>
        public ReporteInfraestructura(
            long id,
            long idMapInstitucionPeriodo,
            int intTotalAulas,
            int intTotalLaboratorios,
            int intTotalTalleres,
            bool bitBiblioteca,
            int intTotalBibliotecas,
            int intTotalComputo,
            long idInternet,
            long idDiscapacitado,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo)
        {
            Id = id;
            IdMapInstitucionPeriodo = idMapInstitucionPeriodo;
            IntTotalAulas = intTotalAulas;
            IntTotalLaboratorios = intTotalLaboratorios;
            IntTotalTalleres = intTotalTalleres;
            BitBiblioteca = bitBiblioteca;
            IntTotalBibliotecas = intTotalBibliotecas;
            IntTotalComputo = intTotalComputo;
            IdInternet = idInternet;
            IdDiscapacitado = idDiscapacitado;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
        }

        #endregion
    }
}