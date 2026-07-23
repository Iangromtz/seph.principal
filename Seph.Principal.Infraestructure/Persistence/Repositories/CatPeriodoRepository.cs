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
    public sealed class CatPeriodoRepository : ICatPeriodoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public CatPeriodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        public async Task<IReadOnlyList<CatPeriodo>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatPeriodos
                .AsNoTracking()
                .Include(x => x.TipoPeriodo)
                .OrderBy(x => x.IntAnio)
                .ThenBy(x => x.IntNumeroPeriodo)
                .ToListAsync(cancellationToken);
        }

        public async Task<CatPeriodo?> GetByIdAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return await _context.CatPeriodos
                .Include(x => x.TipoPeriodo)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
        /* Valida si ya existe un periodo con el mismo año
y número de periodo. */
        public async Task<bool> ExistsByAnioNumeroPeriodoAsync(
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.CatPeriodos
                .AsNoTracking()
                .AnyAsync(
                    x => x.IntAnio == intAnio
                        && x.IntNumeroPeriodo == intNumeroPeriodo,
                    cancellationToken);
        }
        /* Valida si existe otro periodo con el mismo año y número,
excluyendo el registro que se está actualizando. */
        public async Task<bool> ExistsByAnioNumeroPeriodoExceptIdAsync(
            int intAnio,
            int intNumeroPeriodo,
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatPeriodos
                .AsNoTracking()
                .AnyAsync(
                    x => x.IntAnio == intAnio
                        && x.IntNumeroPeriodo == intNumeroPeriodo
                        && x.Id != id,
                    cancellationToken);
        }
        public async Task AddAsync(CatPeriodo catPeriodo, CancellationToken cancellationToken)
        {
            await _context.CatPeriodos.AddAsync(
                catPeriodo,
                cancellationToken);
        }

        public void Update(CatPeriodo catPeriodo)
        {
            _context.CatPeriodos.Update(catPeriodo);
        }

        public void Delete(CatPeriodo catPeriodo)
        {
            _context.CatPeriodos.Remove(catPeriodo);
        }

        #endregion
    }
}
