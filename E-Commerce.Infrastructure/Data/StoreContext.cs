using E_Commerce.Core.Entities;
using E_Commerce.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Data
{
    public class StoreContext(DbContextOptions dbContext) : DbContext(dbContext)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //to get migration file of the product from the config
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}
