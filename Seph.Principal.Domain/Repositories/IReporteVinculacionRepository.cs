using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los reportes de vinculación.
    /// </summary>
    public interface IReporteVinculacionRepository
    {
        /// <summary>
        /// Obtiene todos los reportes de vinculación.
        /// </summary>
        Task<IReadOnlyList<ReporteVinculacion>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene un reporte de vinculación por su identificador.
        /// </summary>
        Task<ReporteVinculacion?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte asociado a una institución
        /// dentro de un periodo específico.
        /// </summary>
        Task<ReporteVinculacion?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        Task<ReporteVinculacion?> GetByMapInstitucionPeriodoForUpdateAsync(
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
        /// Agrega un nuevo reporte de vinculación.
        /// </summary>
        Task AddAsync(
            ReporteVinculacion reporteVinculacion,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte del periodo anterior
        /// correspondiente a una institución.
        /// </summary>
        Task<ReporteVinculacion?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un reporte de vinculación para actualización.
        /// </summary>
        void Update(ReporteVinculacion reporteVinculacion);

        /// <summary>
        /// Marca un reporte de vinculación para eliminación.
        /// </summary>
        void Delete(ReporteVinculacion reporteVinculacion);
    }
}