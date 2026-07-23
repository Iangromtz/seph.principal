using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatTipoPeriodoRepository
    {
        Task<IReadOnlyList<CatTipoPeriodo>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<CatTipoPeriodo?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /* Valida si ya existe un tipo de periodo
        con el mismo valor. */
        Task<bool> ExistsByValorAsync(
            string strValor,
            CancellationToken cancellationToken);

        /* Valida si existe otro tipo de periodo con el mismo valor,
        excluyendo el registro que se está editando. */
        Task<bool> ExistsByValorExceptIdAsync(
            string strValor,
            long id,
            CancellationToken cancellationToken);

        Task AddAsync(
            CatTipoPeriodo catTipoPeriodo,
            CancellationToken cancellationToken);

        void Update(CatTipoPeriodo catTipoPeriodo);

        void Delete(CatTipoPeriodo catTipoPeriodo);
    }
}
