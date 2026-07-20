using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatInternetRepository
        : ICatInternetRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public CatInternetRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodos de la clase

        public async Task<IReadOnlyList<CatInternet>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.CatInternets
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }

        public async Task<CatInternet?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatInternets
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(
            CatInternet internet,
            CancellationToken cancellationToken)
        {
            await _context.CatInternets.AddAsync(
                internet,
                cancellationToken);
        }

        #endregion
    }
}