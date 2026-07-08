using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class MapUserPerfilAcademicoRepository(ApplicationDbContext context) : IMapUserPerfilAcademicoRepository
    {
        public async Task<IReadOnlyList<MapUserPerfilAcademico>> GetByUserIdAsync(Guid idUser, CancellationToken cancellationToken)
        {
            return await context.MapUserPerfilesAcademicos
                .AsNoTracking()
                .Where(x => x.IdAspNetUsers == idUser)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(MapUserPerfilAcademico mapa, CancellationToken cancellationToken)
        {
            await context.MapUserPerfilesAcademicos.AddAsync(mapa, cancellationToken);
        }

        public async Task DeleteByUserIdAsync(Guid idUser, CancellationToken cancellationToken)
        {
            var existentes = await context.MapUserPerfilesAcademicos
                .Where(x => x.IdAspNetUsers == idUser)
                .ToListAsync(cancellationToken);

            context.MapUserPerfilesAcademicos.RemoveRange(existentes);
        }
    }
}
