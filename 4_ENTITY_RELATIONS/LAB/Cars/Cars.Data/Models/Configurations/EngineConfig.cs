using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Data.Models.Configurations
{
    class EngineConfig : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder                                           // one -> many
                .HasMany(c => c.Cars)
                .WithOne(e => e.Engine)
                .HasForeignKey(e => e.EngineId);

        }
    }
}
