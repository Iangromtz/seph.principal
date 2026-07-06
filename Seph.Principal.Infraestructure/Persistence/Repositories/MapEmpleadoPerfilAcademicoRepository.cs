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
    public sealed class MapEmpleadoPerfilAcademicoRepository : IMapEmpleadoPerfilAcademicoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public MapEmpleadoPerfilAcademicoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<MapEmpleadoPerfilAcademico>> GetByEmpleadoIdAsync(long idEmpleado, CancellationToken cancellationToken)
        {
            return await _context.MapEmpleadoPerfilesAcademicos
                .AsNoTracking()
                .Where(x => x.IdEmpleado == idEmpleado)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(MapEmpleadoPerfilAcademico mapa, CancellationToken cancellationToken)
        {
            await _context.MapEmpleadoPerfilesAcademicos.AddAsync(
                mapa,
                cancellationToken);
        }

        public async Task DeleteByEmpleadoIdAsync(long idEmpleado, CancellationToken cancellationToken)
        {
            var existentes = await _context.MapEmpleadoPerfilesAcademicos
                .Where(x => x.IdEmpleado == idEmpleado)
                .ToListAsync(cancellationToken);

            _context.MapEmpleadoPerfilesAcademicos.RemoveRange(existentes);
        }
        #endregion
    }
}
