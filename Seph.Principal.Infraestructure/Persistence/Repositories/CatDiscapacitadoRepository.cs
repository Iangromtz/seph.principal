using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatDiscapacitadoRepository
        : ICatDiscapacitadoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public CatDiscapacitadoRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodos de la clase

        public async Task<IReadOnlyList<CatDiscapacitado>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.CatDiscapacitados
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }

        public async Task<CatDiscapacitado?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatDiscapacitados
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(
            CatDiscapacitado discapacitado,
            CancellationToken cancellationToken)
        {
            await _context.CatDiscapacitados.AddAsync(
                discapacitado,
                cancellationToken);
        }

        #endregion
    }
}