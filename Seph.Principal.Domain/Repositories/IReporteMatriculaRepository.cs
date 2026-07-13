using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IReporteMatriculaRepository
    {
        Task<IReadOnlyList<ReporteMatricula>> GetAllAsync(CancellationToken cancellationToken);

        Task<ReporteMatricula?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<ReporteMatricula?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        Task AddAsync(ReporteMatricula reporteMatricula, CancellationToken cancellationToken);


        Task<ReporteMatricula?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        void Update(ReporteMatricula reporteMatricula);

        void Delete(ReporteMatricula reporteMatricula);


    }
}
