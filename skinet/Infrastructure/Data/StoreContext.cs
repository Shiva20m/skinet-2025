using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Config;

namespace Infrastructure.Data
{
    public class StoreContext(DbContextOptions options): DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // move this logic to configuration
            // modelBuilder.Entity<Product>().Property(x=> x.Price).HasColumnName(18,2);
            base.OnModelCreating(modelBuilder);
            // where is config located
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}
