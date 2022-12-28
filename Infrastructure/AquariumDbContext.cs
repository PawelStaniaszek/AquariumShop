using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AquariumDbContext : IdentityDbContext<ApiUser>
    {
        public AquariumDbContext(DbContextOptions<AquariumDbContext> options) : base(options) { }

        public DbSet<Product>? Products { get; set; }

        public DbSet<Category>? Categories { get; set; }
    }
}
