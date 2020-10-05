using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain;

namespace ProductManagement.Persistence.Mapping
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).HasColumnName("Name");
            builder.Property(a => a.Description).HasColumnName("Description");
        }
    }
}
