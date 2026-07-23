using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatMecanismoSeguimientoRepository : ICatMecanismoSeguimientoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatMecanismoSeguimientoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatMecanismoSeguimiento>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatMecanismos
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatMecanismoSeguimiento?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatMecanismos
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatMecanismoSeguimiento mecanismos, CancellationToken cancellationToken)
        {
            await _context.CatMecanismos.AddAsync(
                mecanismos,
                cancellationToken);
        }

        #endregion
    }
}