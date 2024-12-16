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
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }

        public UniqloDbContext(DbContextOptions opt) : base(opt) { }
    }
}
