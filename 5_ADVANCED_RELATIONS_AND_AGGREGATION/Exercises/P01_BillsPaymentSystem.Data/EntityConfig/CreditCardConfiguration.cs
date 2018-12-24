using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditcardConfiguration : IEntityTypeConfiguration<CreditCard>
    {             
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(e => e.CreditCardId);

            builder
                .Ignore(c => c.LimitLeft);

            builder
                .Ignore(c => c.PaymentMethodId);
        }
    }
}
