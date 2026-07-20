using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatInternetRepository
    {
        Task<IReadOnlyList<CatInternet>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatInternet?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatInternet internet, CancellationToken cancellationToken);

    }
}
