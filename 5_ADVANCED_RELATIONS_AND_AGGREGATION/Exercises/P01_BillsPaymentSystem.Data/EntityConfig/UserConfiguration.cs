using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.UserId);

            builder
                .Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder
                .Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);

            builder
                .Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);            
        }      
    }
}
