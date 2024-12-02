using Uniqloo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Uniqloo.DataAccess
{
    public class UniqloDbContext : IdentityDbContext<User>
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public IEnumerable<object> ProductImage { get; internal set; }

        public UniqloDbContext(DbContextOptions opt) :base(opt) { }
    }
}
