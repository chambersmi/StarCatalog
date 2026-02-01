using StarCatalog.Web.Contracts;
using StarCatalog.Web.Models;
using StarCatalog.Web.Repositories;

namespace StarCatalog.Web.Services
{
    public class StarService : IStarService
    {
        private readonly IStarRepository _starRepository;
        private readonly ILogger<StarService> _logger;
        private const int MaxPageSize = 500;

        public StarService(IStarRepository starRepository, ILogger<StarService> logger)
        {
            _starRepository = starRepository;
            _logger = logger;
        }

        public Task<Star?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return _starRepository.GetByIdAsync(id, ct);
        }

        public async Task<PagedResult<Star>> SearchAsync(StarQuery query, CancellationToken ct = default)
        {
            if(query == null)
            {
                query = new StarQuery();
            }

            if(query.Page < 1)
                query.Page = 1;

            if (query.PageSize < 1)
                query.PageSize = 50;

            double? magMin = query.MagMin;
            double? magMax = query.MagMax;
            CheckRangeAndNormalize(ref magMin, ref magMax);
            query.MagMin = magMin;
            query.MagMax = magMax;

            double? distMin = query.DistMin;
            double? distMax = query.DistMax;
            CheckRangeAndNormalize(ref distMin, ref distMax);
            query.DistMin = distMin;
            query.DistMax = distMax;

            if (!string.IsNullOrWhiteSpace(query.Constellation))
                query.Constellation = query.Constellation.Trim().ToUpperInvariant();

            var total = await _starRepository.CountAsync(query, ct);
            var items = await _starRepository.SearchAsync(query, ct);

            return new PagedResult<Star>
            {
                Items = items,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        // Protects from reversed ranges (Example: MagMin = 10, MagMax = 1)
        private void CheckRangeAndNormalize(ref double? magMin, ref double? magMax)
        {
            if(magMin.HasValue && magMax.HasValue)
            {
                if(magMin.Value > magMax.Value)
                {
                    double? tempNum = magMin;
                    magMin = magMax;
                    magMax = tempNum;
                }
            }
        }
    }
}
