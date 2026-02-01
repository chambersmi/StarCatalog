using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StarCatalog.Web.Data;
using StarCatalog.Web.Models;

namespace StarCatalog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _db;

        public List<Star> Stars { get; private set; } = new();

        public IndexModel(ILogger<IndexModel> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Stars = await _db.Stars
                .OrderBy(s => s.Mag)
                .Take(50)
                .ToListAsync();
        }
    }
}
