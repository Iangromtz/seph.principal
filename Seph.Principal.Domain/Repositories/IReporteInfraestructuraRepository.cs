using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los reportes de infraestructura.
    /// </summary>
    public interface IReporteInfraestructuraRepository
    {
        /// <summary>
        /// Obtiene todos los reportes de infraestructura.
        /// </summary>
        Task<IReadOnlyList<ReporteInfraestructura>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene un reporte de infraestructura por su identificador.
        /// </summary>
        Task<ReporteInfraestructura?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte asociado a una institución
        /// dentro de un periodo específico.
        /// </summary>
        Task<ReporteInfraestructura?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        Task<ReporteInfraestructura?> GetByMapInstitucionPeriodoForUpdateAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Verifica si ya existe un reporte para la relación
        /// entre institución y periodo indicada.
        /// </summary>
        Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo reporte de infraestructura.
        /// </summary>
        Task AddAsync(
            ReporteInfraestructura reporteInfraestructura,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte del periodo anterior
        /// correspondiente a una institución.
        /// </summary>
        Task<ReporteInfraestructura?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un reporte de infraestructura para actualización.
        /// </summary>
        void Update(ReporteInfraestructura reporteInfraestructura);

        /// <summary>
        /// Marca un reporte de infraestructura para eliminación.
        /// </summary>
        void Delete(ReporteInfraestructura reporteInfraestructura);
    }
}