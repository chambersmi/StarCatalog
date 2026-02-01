using Microsoft.EntityFrameworkCore;
using StarCatalog.Web.Contracts;
using StarCatalog.Web.Data;
using StarCatalog.Web.Models;

namespace StarCatalog.Web.Repositories
{
    public class StarRepository : IStarRepository
    {
        private readonly AppDbContext _db;

        public StarRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<int> CountAsync(StarQuery query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Star?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _db.Stars
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IReadOnlyList<Star>> SearchAsync(StarQuery query, CancellationToken ct = default)
        {

        }
    }
}
