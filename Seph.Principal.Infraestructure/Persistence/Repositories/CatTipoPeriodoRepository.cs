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
    public sealed class CatTipoPeriodoRepository
      : ICatTipoPeriodoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public CatTipoPeriodoRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        public async Task<IReadOnlyList<CatTipoPeriodo>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.CatTiposPeriodo
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }

        public async Task<CatTipoPeriodo?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatTiposPeriodo
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /* Valida si ya existe un tipo de periodo
        con el mismo valor. */
        public async Task<bool> ExistsByValorAsync(
            string strValor,
            CancellationToken cancellationToken)
        {
            return await _context.CatTiposPeriodo
                .AsNoTracking()
                .AnyAsync(
                    x => x.StrValor == strValor,
                    cancellationToken);
        }

        /* Valida si existe otro tipo de periodo con el mismo valor,
        excluyendo el registro que se está actualizando. */
        public async Task<bool> ExistsByValorExceptIdAsync(
            string strValor,
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatTiposPeriodo
                .AsNoTracking()
                .AnyAsync(
                    x => x.StrValor == strValor
                        && x.Id != id,
                    cancellationToken);
        }

        public async Task AddAsync(
            CatTipoPeriodo catTipoPeriodo,
            CancellationToken cancellationToken)
        {
            await _context.CatTiposPeriodo.AddAsync(
                catTipoPeriodo,
                cancellationToken);
        }

        public void Update(
            CatTipoPeriodo catTipoPeriodo)
        {
            _context.CatTiposPeriodo.Update(
                catTipoPeriodo);
        }

        public void Delete(
            CatTipoPeriodo catTipoPeriodo)
        {
            _context.CatTiposPeriodo.Remove(
                catTipoPeriodo);
        }

        #endregion
    }
}
