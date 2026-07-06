using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatPerfilAcademicoRepository
    {
        Task<IReadOnlyList<CatPerfilAcademico>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatPerfilAcademico?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatPerfilAcademico perfilAcademico, CancellationToken cancellationToken);
    }
}
