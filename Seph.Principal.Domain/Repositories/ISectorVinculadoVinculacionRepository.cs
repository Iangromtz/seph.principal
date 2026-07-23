using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los sectores vinculados de un reporte.
    /// </summary>
    public interface ISectorVinculadoVinculacionRepository
    {
        /// <summary>
        /// Obtiene todos los sectores asociados
        /// a un reporte de vinculación.
        /// </summary>
        Task<IReadOnlyList<SectorVinculadoVinculacion>> GetByIdVinculacionAsync(
            long idVinculacion,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo sector vinculado
        /// al reporte correspondiente.
        /// </summary>
        Task AddAsync(
            SectorVinculadoVinculacion sectorVinculadoVinculacion,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega una colección de sectores
        /// vinculados al reporte correspondiente.
        /// </summary>
        Task AddRangeAsync(
            IEnumerable<SectorVinculadoVinculacion> sectoresVinculados,
            CancellationToken cancellationToken);

        /// <summary>
        /// Elimina todos los sectores asociados
        /// a un reporte de vinculación.
        /// </summary>
        Task DeleteByIdVinculacionAsync(
            long idVinculacion,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un sector vinculado
        /// para eliminación.
        /// </summary>
        void Delete(
            SectorVinculadoVinculacion sectorVinculadoVinculacion);
    }
}