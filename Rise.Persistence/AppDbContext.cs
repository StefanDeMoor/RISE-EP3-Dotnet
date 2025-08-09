using Microsoft.EntityFrameworkCore;
using Rise.Domain.AmountItems;
using Rise.Domain.Categories;
using Rise.Domain.Customer;
using Rise.Domain.Overviews;


namespace Rise.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Overview> Overviews { get; set; }
        public DbSet<AmountItem> AmountItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category → Overviews (1-to-many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Overviews)
                .WithOne(o => o.Category)
                .HasForeignKey(o => o.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Overview → AmountItems (1-to-many)
            modelBuilder.Entity<Overview>()
                .HasMany(o => o.Amounts)
                .WithOne(a => a.Overview)
                .HasForeignKey(a => a.OverviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // AmountItem → SubAmounts (self-referencing)
            modelBuilder.Entity<AmountItem>()
                .HasMany(a => a.SubAmounts)
                .WithOne(a => a.ParentAmountItem)
                .HasForeignKey(a => a.ParentAmountItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
