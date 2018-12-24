using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductsShop.Data.EntityConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                 .HasKey(r => r.Id);

            builder
                .Property(s => s.Name)
                .IsRequired();
        }        
    }
}
