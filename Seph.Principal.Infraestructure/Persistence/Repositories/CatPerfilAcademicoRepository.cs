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
    public sealed class CatPerfilAcademicoRepository : ICatPerfilAcademicoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatPerfilAcademicoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatPerfilAcademico>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatPerfilesAcademicos
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }

        public async Task<CatPerfilAcademico?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatPerfilesAcademicos
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatPerfilAcademico perfilAcademico, CancellationToken cancellationToken)
        {
            await _context.CatPerfilesAcademicos.AddAsync(
                perfilAcademico,
                cancellationToken);
        }
        #endregion
    }
}
