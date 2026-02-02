using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarCatalog.Web.Contracts;
using StarCatalog.Web.Models;
using StarCatalog.Web.Services;

namespace StarCatalog.Web.Pages.Stars
{
    public class IndexModel : PageModel
    {
        private readonly IStarService _starService;
        private readonly ILogger<IndexModel> _logger;
        public PagedResult<Star> Result { get; private set; } = null!;

        public IndexModel(IStarService starService, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _starService = starService;
        }

        public async Task OnGetAsync(int page = 1, int pageSize = 50, string? sortBy = "mag", bool desc = false)
        {
            var query = new StarQuery
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                Desc = desc
            };

            Result = await _starService.SearchAsync(query);
        }
    }
}
