using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatDiscapacitadoRepository
    {
        Task<IReadOnlyList<CatDiscapacitado>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatDiscapacitado?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatDiscapacitado discapacitado, CancellationToken cancellationToken);

    }
}
