using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.EntityConfig;
using ProductsShop.Models;

namespace ProductsShop.Data
{
    public class ProductsShopContext:DbContext
    {
        public ProductsShopContext()
        {
        }
        public ProductsShopContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(DbConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());

            builder.ApplyConfiguration(new CategoryConfig());

            builder.ApplyConfiguration(new ProductConfig());

            builder.ApplyConfiguration(new CategoryProductConfig());
        }
    }
}
