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

        public async Task<int> CountAsync(StarQuery query, CancellationToken ct = default)
        {
            var q = ApplyFilters(_db.Stars.AsNoTracking(), query);
            return await q.CountAsync(ct);
        }

        public async Task<Star?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _db.Stars
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IReadOnlyList<Star>> SearchAsync(StarQuery query, CancellationToken ct = default)
        {
            var q = ApplyFilters(_db.Stars.AsNoTracking(), query);
            q = ApplySorting(q, query);

            int page = query.Page;
            if(page < 1)
            {
                page = 1;
            }

            int pageSize = query.PageSize;
            if(pageSize < 1)
            {
                pageSize = 50;
            }

            return await q
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        private static IQueryable<Star> ApplyFilters(IQueryable<Star> q, StarQuery query)
        {
            if(!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = query.Search.Trim();
                q = q.Where(s => s.Proper != null && EF.Functions.Like(s.Proper, $"%{search}"));
            }

            if(!string.IsNullOrWhiteSpace(query.Constellation))
            {
                var constellation = query.Constellation.Trim();
                q = q.Where(s => s.Con == constellation);
            }

            if(query.MagMax.HasValue)
                q = q.Where(s => s.Mag.HasValue && s.Mag.Value <= query.MagMax.Value);

            if(query.MagMin.HasValue)
                q = q.Where(s => s.Mag.HasValue && s.Mag.Value >= query.MagMin.Value);

            if(query.DistMin.HasValue)            
                q = q.Where(s => s.Dist.HasValue && s.Dist.Value >= query.DistMin.Value);

            if (query.DistMax.HasValue)
                q = q.Where(s => s.Dist.HasValue && s.Dist.Value <= query.DistMax.Value);

            if (query.OnlyWithHrData)
                q = q.Where(s => s.CI.HasValue && s.AbsMag.HasValue);

            return q;
        }

        private static IQueryable<Star> ApplySorting(IQueryable<Star> q, StarQuery query)
        {
            var sortBy = (query.SortBy ?? "mag").Trim().ToLowerInvariant();
            var desc = query.Desc;

            switch(sortBy)
            {
                case "Id":
                    q = desc ? q.OrderByDescending(s => s.Id)
                        : q.OrderBy(s => s.Id);
                    break;
                case "Proper":
                    q = desc ? q.OrderByDescending(s => s.Proper)
                        : q.OrderBy(s => s.Proper);
                    break;
                case "Mag":
                    q = desc ? q.OrderByDescending(s => s.Mag)
                        : q.OrderBy(s => s.Mag);
                    break;
                case "AbsMag":
                    q = desc ? q.OrderByDescending(s => s.AbsMag)
                        : q.OrderBy(s => s.AbsMag);
                    break;
                case "Dist":
                    q = desc ? q.OrderByDescending(s => s.Dist)
                        : q.OrderBy(s => s.Dist);
                    break;
                case "Con":
                    q = desc ? q.OrderByDescending(s => s.Con)
                        : q.OrderBy(s => s.Con);
                    break;
                default:
                    q = desc ? q.OrderByDescending(s => s.Mag)
                        : q.OrderBy(s => s.Mag);
                    break;
            }

            return q;
        }
    }
}
