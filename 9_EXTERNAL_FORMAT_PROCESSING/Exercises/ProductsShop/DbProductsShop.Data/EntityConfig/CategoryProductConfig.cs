using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ProductsShop.Data.EntityConfig
{
    public class CategoryProductConfig : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder
               .HasKey(pt => new { pt.ProductId, pt.CategoryId });

            builder
                .HasOne(pt => pt.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(c => c.ProductId);

            builder
                .HasOne(pt => pt.Category)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
