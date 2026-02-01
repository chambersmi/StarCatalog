using Microsoft.EntityFrameworkCore;
using StarCatalog.Web.Models;

namespace StarCatalog.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Star> Stars { get; set; }
    }
}
