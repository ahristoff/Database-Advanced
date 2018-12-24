using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductsShop.Data.EntityConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.BuyerId)
                .IsRequired(false);

            builder.Property(e => e.Price)
                .IsRequired();
        }
       
    }
}
