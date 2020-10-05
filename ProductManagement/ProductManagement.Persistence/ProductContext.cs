using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using ProductManagement.Persistence.Mapping;

namespace ProductManagement.Persistence
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }


        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurations(typeof(ProductMapping).Assembly);

        }
    }
}
