using StarCatalog.Web.Contracts;
using StarCatalog.Web.Models;

namespace StarCatalog.Web.Services
{
    public class StarService : IStarService
    {
        public Task<Star?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Star>> SearchAsync(StarQuery query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
