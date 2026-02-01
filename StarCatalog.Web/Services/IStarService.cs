using StarCatalog.Web.Contracts;
using StarCatalog.Web.Models;

namespace StarCatalog.Web.Services
{
    public interface IStarService
    {
        Task<Star?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<PagedResult<Star>> SearchAsync(StarQuery query, CancellationToken ct = default);
    }
}
