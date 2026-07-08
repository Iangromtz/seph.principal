using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IMapUserPerfilAcademicoRepository
    {
        Task<IReadOnlyList<MapUserPerfilAcademico>> GetByUserIdAsync(Guid idUser, CancellationToken cancellationToken);
        Task AddAsync(MapUserPerfilAcademico mapa, CancellationToken cancellationToken);
        Task DeleteByUserIdAsync(Guid idUser, CancellationToken cancellationToken);
    }
}
