using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los sectores vinculados de un reporte.
    /// </summary>
    public sealed class SectorVinculadoVinculacionRepository
        : ISectorVinculadoVinculacionRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public SectorVinculadoVinculacionRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los sectores asociados
        /// a un reporte de vinculación.
        /// </summary>
        public async Task<IReadOnlyList<SectorVinculadoVinculacion>>
            GetByIdVinculacionAsync(
                long idVinculacion,
                CancellationToken cancellationToken)
        {
            return await _context.SectorVinculadoVinculaciones
                .AsNoTracking()
                .Where(x => x.IdVinculacion == idVinculacion)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo sector vinculado
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            SectorVinculadoVinculacion sectorVinculadoVinculacion,
            CancellationToken cancellationToken)
        {
            await _context.SectorVinculadoVinculaciones.AddAsync(
                sectorVinculadoVinculacion,
                cancellationToken);
        }

        /// <summary>
        /// Agrega una colección de sectores
        /// vinculados al contexto de la aplicación.
        /// </summary>
        public async Task AddRangeAsync(
            IEnumerable<SectorVinculadoVinculacion> sectoresVinculados,
            CancellationToken cancellationToken)
        {
            await _context.SectorVinculadoVinculaciones.AddRangeAsync(
                sectoresVinculados,
                cancellationToken);
        }

        /// <summary>
        /// Elimina todos los sectores asociados
        /// a un reporte de vinculación.
        /// </summary>
        public async Task DeleteByIdVinculacionAsync(
            long idVinculacion,
            CancellationToken cancellationToken)
        {
            var registros = await _context.SectorVinculadoVinculaciones
                .Where(x => x.IdVinculacion == idVinculacion)
                .ToListAsync(cancellationToken);

            _context.SectorVinculadoVinculaciones.RemoveRange(registros);
        }

        /// <summary>
        /// Marca un sector vinculado
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            SectorVinculadoVinculacion sectorVinculadoVinculacion)
        {
            _context.SectorVinculadoVinculaciones.Remove(
                sectorVinculadoVinculacion);
        }

        #endregion
    }
}