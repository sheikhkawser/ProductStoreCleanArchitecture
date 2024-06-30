using Microsoft.EntityFrameworkCore;
using ProductStore.Core.Entities;

namespace ProductStore.Infrastructure
{
    public class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext(DbContextOptions<ProductStoreDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
