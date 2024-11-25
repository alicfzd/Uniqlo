using Uniqloo.Models;
using Microsoft.EntityFrameworkCore;

namespace Uniqloo.DataAccess
{
    public class UniqloDbContext : DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public UniqloDbContext(DbContextOptions opt) :base(opt) { }
    }
}
