using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class ReportePersonalRepository : IReportePersonalRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public ReportePersonalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        public async Task<IReadOnlyList<ReportePersonal>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.ReportePersonales
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReportePersonal?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePersonales
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<ReportePersonal?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePersonales
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        public async Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePersonales
                .AnyAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        public async Task AddAsync(
            ReportePersonal reportePersonal,
            CancellationToken cancellationToken)
        {
            await _context.ReportePersonales.AddAsync(
                reportePersonal,
                cancellationToken);
        }

        public async Task<ReportePersonal?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken)


        {
            return await _context.ReportePersonales
                .AsNoTracking()
                .Join(
                    _context.MapInstitucionPeriodos,
                    reporte => reporte.IdMapInstitucionPeriodo,
                    map => map.Id,
                    (reporte, map) => new { reporte, map })
                .Join(
                    _context.CatPeriodos,
                    rp => rp.map.IdPeriodo,
                    periodo => periodo.Id,
                    (rp, periodo) => new { rp.reporte, rp.map, periodo })
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
               public async Task<ReportePersonal?> GetByMapInstitucionPeriodoForUpdateAsync(
             long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePersonales
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }
        public void Update(ReportePersonal reportePersonal)
        {
            _context.ReportePersonales.Update(reportePersonal);
        }

        public void Delete(ReportePersonal reportePersonal)
        {
            _context.ReportePersonales.Remove(reportePersonal);
        }

        #endregion
    }
}