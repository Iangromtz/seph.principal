using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatPeriodoRepository
    {
        Task<IReadOnlyList<CatPeriodo>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatPeriodo?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatPeriodo catPeriodo, CancellationToken cancellationToken);

        void Update(CatPeriodo catPeriodo);

        void Delete(CatPeriodo catPeriodo);
    }
}
