using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {      
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);
          
            builder.HasIndex(c => new { c.UserId, c.BankAccountId, c.CreditCardId }).IsUnique();

            builder
                .HasOne(c => c.User)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(c => c.UserId);

            builder
                .HasOne(c => c.BankAccount)
                .WithOne(c => c.PaymentMethod)
                .HasForeignKey<PaymentMethod>(c => c.BankAccountId);

            builder
               .HasOne(c => c.CreditCard)
               .WithOne(c => c.PaymentMethod)
               .HasForeignKey<PaymentMethod>(c => c.CreditCardId);
        }
    }
}
