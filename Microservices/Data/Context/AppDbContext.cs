using Microsoft.EntityFrameworkCore;
using Services.Platforms.Data.Models;

namespace Services.Platforms.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
