using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatMunicipioRepository : ICatMunicipioRepository
    {
        private readonly ApplicationDbContext _context;

        public CatMunicipioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<CatMunicipio>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatMunicipios
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
    }
}
