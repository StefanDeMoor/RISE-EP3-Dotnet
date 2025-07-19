using Microsoft.EntityFrameworkCore;
using Rise.Domain.Categories;

namespace Rise.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}

