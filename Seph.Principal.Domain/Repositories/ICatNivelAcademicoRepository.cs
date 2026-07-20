using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatNivelAcademicoRepository
    {
        Task<IReadOnlyList<CatNivelAcademico>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatNivelAcademico?> GetByIdAsync(long id,CancellationToken cancellationToken);
    }
}
