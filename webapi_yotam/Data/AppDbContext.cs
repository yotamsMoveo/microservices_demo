using Microsoft.EntityFrameworkCore;
using webapi_yotam.Models;

namespace webapi_yotam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {


        }
        public DbSet<Platform> Platforms { get; set; }

    }
}
