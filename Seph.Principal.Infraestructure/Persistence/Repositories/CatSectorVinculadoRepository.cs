using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatSectorVinculadoRepository : ICatSectorVinculadoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatSectorVinculadoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatSectorVinculado>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatSectores
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatSectorVinculado?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatSectores
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatSectorVinculado sector, CancellationToken cancellationToken)
        {
            await _context.CatSectores.AddAsync(
                sector,
                cancellationToken);
        }

        #endregion
    }
}