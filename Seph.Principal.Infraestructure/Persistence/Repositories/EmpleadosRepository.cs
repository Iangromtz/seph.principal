using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly ApplicationDbContext _context;
        #region Constructor
        public EmpleadosRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        #endregion

        #region Metodos de la clase

        public async Task<IReadOnlyList<Empleado>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Empleados
                .AsNoTracking()
                .OrderBy(x => x.StrNombre)
                .ToListAsync(cancellationToken);
        }

        public async Task<Empleado?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
        }

        public async Task AddAsync(Empleado empleados, CancellationToken cancellationToken)
        {
            await _context.Empleados.AddAsync(
                empleados,
                cancellationToken);
        }

        public void Update(Empleado empleados)
        {
            _context.Empleados.Update(empleados);
        }

        public void Delete(Empleado empleados)
        {
            _context.Empleados.Remove(empleados);
        }
        #endregion

    }
}
