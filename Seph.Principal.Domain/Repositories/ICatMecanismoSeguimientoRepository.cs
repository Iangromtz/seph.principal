using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatMecanismoSeguimientoRepository
    {
        Task<IReadOnlyList<CatMecanismoSeguimiento>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatMecanismoSeguimiento?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatMecanismoSeguimiento mecanismo, CancellationToken cancellationToken);

    }
}
