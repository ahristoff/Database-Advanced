using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .Property(c => c.Name)
                .HasMaxLength(50)                
                //.HasColumnType("char")
                .IsUnicode();

            builder.Entity<Product>()
               .Property(s => s.Description)
               .HasDefaultValue("No description");

            builder.Entity<Customer>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsUnicode();

            builder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(80)
                .IsUnicode(false);

            builder.Entity<Store>()
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsUnicode();

            builder.Entity<Store>()
                .HasAlternateKey(s => s.Name);//unique 

            builder.Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Sale>()
                .HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.CustomerId);

            builder.Entity<Sale>()
                .HasOne(c => c.Product)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.ProductId);

            builder.Entity<Sale>()
                .HasOne(c => c.Store)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.StoreId);                
        }
    }
}
