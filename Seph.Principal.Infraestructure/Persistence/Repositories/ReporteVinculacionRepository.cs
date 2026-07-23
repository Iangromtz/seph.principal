using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los reportes de vinculación.
    /// </summary>
    public sealed class ReporteVinculacionRepository
        : IReporteVinculacionRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public ReporteVinculacionRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los reportes de vinculación
        /// sin habilitar el seguimiento de cambios.
        /// </summary>
        public async Task<IReadOnlyList<ReporteVinculacion>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.ReporteVinculaciones
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte de vinculación
        /// mediante su identificador.
        /// </summary>
        public async Task<ReporteVinculacion?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteVinculaciones
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte correspondiente a una relación
        /// entre institución y periodo.
        /// </summary>
        public async Task<ReporteVinculacion?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteVinculaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte con seguimiento habilitado
        /// para permitir su actualización.
        /// </summary>
        public async Task<ReporteVinculacion?>
            GetByMapInstitucionPeriodoForUpdateAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReporteVinculaciones
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Verifica si ya existe un reporte para la relación
        /// entre institución y periodo indicada.
        /// </summary>
        public async Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteVinculaciones
                .AnyAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo reporte de vinculación
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            ReporteVinculacion reporteVinculacion,
            CancellationToken cancellationToken)
        {
            await _context.ReporteVinculaciones.AddAsync(
                reporteVinculacion,
                cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte de vinculación más reciente
        /// perteneciente a un periodo anterior.
        /// </summary>
        public async Task<ReporteVinculacion?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteVinculaciones
                .AsNoTracking()
                .Join(
                    _context.MapInstitucionPeriodos,
                    reporte => reporte.IdMapInstitucionPeriodo,
                    map => map.Id,
                    (reporte, map) => new
                    {
                        reporte,
                        map
                    })
                .Join(
                    _context.CatPeriodos,
                    reporteMap => reporteMap.map.IdPeriodo,
                    periodo => periodo.Id,
                    (reporteMap, periodo) => new
                    {
                        reporteMap.reporte,
                        reporteMap.map,
                        periodo
                    })
                .Where(x =>
                    x.map.IdInstitucion == idInstitucion &&
                    (
                        x.periodo.IntAnio < intAnio ||
                        x.periodo.IntAnio == intAnio &&
                        x.periodo.IntNumeroPeriodo < intNumeroPeriodo
                    ))
                .OrderByDescending(x => x.periodo.IntAnio)
                .ThenByDescending(x => x.periodo.IntNumeroPeriodo)
                .Select(x => x.reporte)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Marca un reporte de vinculación
        /// para que sus cambios sean actualizados.
        /// </summary>
        public void Update(
            ReporteVinculacion reporteVinculacion)
        {
            _context.ReporteVinculaciones.Update(
                reporteVinculacion);
        }

        /// <summary>
        /// Marca un reporte de vinculación
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            ReporteVinculacion reporteVinculacion)
        {
            _context.ReporteVinculaciones.Remove(
                reporteVinculacion);
        }

        #endregion
    }
}