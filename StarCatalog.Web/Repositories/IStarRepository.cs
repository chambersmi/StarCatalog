using StarCatalog.Web.Contracts;
using StarCatalog.Web.Models;

namespace StarCatalog.Web.Repositories
{
    public interface IStarRepository
    {
        Task<Star?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Star>> SearchAsync(StarQuery query, CancellationToken ct = default);
        Task<int> CountAsync(StarQuery query, CancellationToken ct = default);
    }
}
