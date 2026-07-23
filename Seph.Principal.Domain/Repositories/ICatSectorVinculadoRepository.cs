using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatSectorVinculadoRepository
    {
        Task<IReadOnlyList<CatSectorVinculado>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatSectorVinculado?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatSectorVinculado sector, CancellationToken cancellationToken);

    }
}
