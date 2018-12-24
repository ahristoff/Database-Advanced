using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {       
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(c => c.BankAccountId);
           
            builder
                .Property(c => c.BankName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder
                .Property(c => c.SWIFTCode)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode(false);

            builder
               .Ignore(c => c.PaymentMethodId);           
        }       
    }
}
