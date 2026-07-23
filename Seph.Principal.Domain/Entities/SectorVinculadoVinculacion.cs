using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa la relación entre un reporte de
    /// vinculación y los sectores con los que
    /// la institución mantiene vinculación.
    /// </summary>
    public class SectorVinculadoVinculacion
    {
        /// <summary>
        /// Identificador único del registro.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador del reporte de vinculación.
        /// </summary>
        public long IdVinculacion { get; set; }

        /// <summary>
        /// Identificador del sector vinculado.
        /// </summary>
        public long IdSectorVinculado { get; set; }

        /// <summary>
        /// Especifica otro sector cuando aplica.
        /// </summary>
        public string? StrOtros { get; set; }

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia vacía
        /// de la entidad SectorVinculadoVinculacion.
        /// </summary>
        public SectorVinculadoVinculacion()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa del registro.
        /// </summary>
        public SectorVinculadoVinculacion(
            long id,
            long idVinculacion,
            long idSectorVinculado,
            string? strOtros)
        {
            Id = id;
            IdVinculacion = idVinculacion;
            IdSectorVinculado = idSectorVinculado;
            StrOtros = strOtros;
        }

        #endregion
    }
}