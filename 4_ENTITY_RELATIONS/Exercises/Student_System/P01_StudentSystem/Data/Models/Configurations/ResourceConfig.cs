using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Models.Configurations
{
    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsUnicode();

            builder
               .Property(r => r.Url)
               .IsUnicode(false);
        }
    }
}
