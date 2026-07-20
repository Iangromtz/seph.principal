using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IReportePersonalRepository
    {
        Task<IReadOnlyList<ReportePersonal>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<ReportePersonal?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        Task<ReportePersonal?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);
        Task<ReportePersonal?> GetByMapInstitucionPeriodoForUpdateAsync(
          long idMapInstitucionPeriodo,
          CancellationToken cancellationToken);

        Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        Task AddAsync(
            ReportePersonal reportePersonal,
            CancellationToken cancellationToken);

        Task<ReportePersonal?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        void Update(ReportePersonal reportePersonal);

        void Delete(ReportePersonal reportePersonal);
    }
}