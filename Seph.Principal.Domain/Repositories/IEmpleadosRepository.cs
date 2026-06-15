using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IEmpleadosRepository
    {
        Task<IReadOnlyList<Empleado>> GetAllAsync(CancellationToken cancellationToken);
        Task<Empleado> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task AddAsync (Empleado empleados, CancellationToken cancellationToken);
        void Update(Empleado empleados);
        void Delete(Empleado empleados);

    }
}
