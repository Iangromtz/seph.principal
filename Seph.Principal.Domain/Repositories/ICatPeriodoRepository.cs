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

        /* Valida si ya existe un periodo con el mismo año
        y número de periodo, para evitar registros duplicados. */
        Task<bool> ExistsByAnioNumeroPeriodoAsync(
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        /* Valida si existe otro periodo con el mismo año y número,
excluyendo el registro que actualmente se está editando. */
        Task<bool> ExistsByAnioNumeroPeriodoExceptIdAsync(
            int intAnio,
            int intNumeroPeriodo,
            long id,
            CancellationToken cancellationToken);
        Task AddAsync(CatPeriodo catPeriodo, CancellationToken cancellationToken);

        void Update(CatPeriodo catPeriodo);

        void Delete(CatPeriodo catPeriodo);
    }
}
