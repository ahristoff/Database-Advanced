using Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Data
{
    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
        {
        }
        public EmployeesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

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
            builder.Entity<Employee>(e =>
            {
                e.HasKey(u => u.Id);

                e.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(60);

                e.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(60);

                e.Property(u => u.Address)
                .HasMaxLength(250);              
            });          
        }
    }
}
