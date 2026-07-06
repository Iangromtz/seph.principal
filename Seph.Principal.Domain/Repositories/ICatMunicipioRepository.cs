using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatMunicipioRepository
    {
        Task<IReadOnlyList<CatMunicipio>> GetAllAsync(CancellationToken cancellationToken);
    }
}
