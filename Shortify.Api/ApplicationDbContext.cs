using Microsoft.EntityFrameworkCore;
using Shortify.Api.Entity;

namespace Shortify.Api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
