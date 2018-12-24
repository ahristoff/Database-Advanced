using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductsShop.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {      
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(d => d.LastName)
                .IsRequired();

            builder
                .HasMany(p => p.ProductBuyers)
                .WithOne(u => u.Buyer)
                .HasForeignKey(f => f.BuyerId);


            builder
                .HasMany(p => p.ProductSellers)
                .WithOne(u => u.Seller)
                .HasForeignKey(f => f.SellerId);
                
        }
    }
}
