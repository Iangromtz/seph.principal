using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface IMapEmpleadoPerfilAcademicoRepository
    {
        Task<IReadOnlyList<MapEmpleadoPerfilAcademico>> GetByEmpleadoIdAsync(long idEmpleado, CancellationToken cancellationToken);

        Task AddAsync(MapEmpleadoPerfilAcademico mapa, CancellationToken cancellationToken);

        /// <summary>
        /// Elimina todos los perfiles académicos previamente asociados a un empleado.
        /// Se usa antes de volver a insertarlos, para que la actualización sea idempotente.
        /// </summary>
        Task DeleteByEmpleadoIdAsync(long idEmpleado, CancellationToken cancellationToken);
    }
}
