using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los reportes de infraestructura.
    /// </summary>
    public sealed class ReporteInfraestructuraRepository
        : IReporteInfraestructuraRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public ReporteInfraestructuraRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los reportes de infraestructura
        /// sin habilitar el seguimiento de cambios.
        /// </summary>
        public async Task<IReadOnlyList<ReporteInfraestructura>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.ReporteInfraestructuras
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte de infraestructura
        /// mediante su identificador.
        /// </summary>
        public async Task<ReporteInfraestructura?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteInfraestructuras
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte correspondiente a una relación
        /// entre institución y periodo.
        /// </summary>
        public async Task<ReporteInfraestructura?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteInfraestructuras
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte con seguimiento habilitado
        /// para permitir su actualización.
        /// </summary>
        public async Task<ReporteInfraestructura?>
            GetByMapInstitucionPeriodoForUpdateAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReporteInfraestructuras
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
            return await _context.ReporteInfraestructuras
                .AnyAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo reporte de infraestructura
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            ReporteInfraestructura reporteInfraestructura,
            CancellationToken cancellationToken)
        {
            await _context.ReporteInfraestructuras.AddAsync(
                reporteInfraestructura,
                cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte de infraestructura más reciente
        /// perteneciente a un periodo anterior.
        /// </summary>
        public async Task<ReporteInfraestructura?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteInfraestructuras
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
        /// Marca un reporte de infraestructura
        /// para que sus cambios sean actualizados.
        /// </summary>
        public void Update(
            ReporteInfraestructura reporteInfraestructura)
        {
            _context.ReporteInfraestructuras.Update(
                reporteInfraestructura);
        }

        /// <summary>
        /// Marca un reporte de infraestructura
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            ReporteInfraestructura reporteInfraestructura)
        {
            _context.ReporteInfraestructuras.Remove(
                reporteInfraestructura);
        }

        #endregion
    }
}