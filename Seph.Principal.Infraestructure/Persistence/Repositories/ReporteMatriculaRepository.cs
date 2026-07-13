using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class ReporteMatriculaRepository : IReporteMatriculaRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public ReporteMatriculaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        public async Task<IReadOnlyList<ReporteMatricula>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ReporteMatriculas
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReporteMatricula?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.ReporteMatriculas
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteMatriculas
                .AnyAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        public async Task AddAsync(ReporteMatricula reporteMatricula, CancellationToken cancellationToken)
        {
            await _context.ReporteMatriculas.AddAsync(
                reporteMatricula,
                cancellationToken);
        }

        public void Update(ReporteMatricula reporteMatricula)
        {
            _context.ReporteMatriculas.Update(reporteMatricula);
        }

        public void Delete(ReporteMatricula reporteMatricula)
        {
            _context.ReporteMatriculas.Remove(reporteMatricula);
        }
        public async Task<ReporteMatricula?> GetByMapInstitucionPeriodoAsync(
    long idMapInstitucionPeriodo,
    CancellationToken cancellationToken)
        {
            return await _context.ReporteMatriculas
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        public async Task<ReporteMatricula?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteMatriculas
                .AsNoTracking()
                .Join(
                    _context.MapInstitucionPeriodos,
                    reporte => reporte.IdMapInstitucionPeriodo,
                    map => map.Id,
                    (reporte, map) => new { reporte, map })
                .Join(
                    _context.CatPeriodos,
                    rm => rm.map.IdPeriodo,
                    periodo => periodo.Id,
                    (rm, periodo) => new { rm.reporte, rm.map, periodo })
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
        #endregion
    }
}
