using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatNivelAcademicoRepository: ICatNivelAcademicoRepository
    {
        private readonly ApplicationDbContext _context;

        public CatNivelAcademicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<CatNivelAcademico>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatNivelAcademicos
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
    }
}
