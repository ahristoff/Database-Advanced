using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasIndex(b => b.Name)                 
                .IsUnique();
            builder.Property(c => c.Name).IsRequired();

            builder
               .Property(s => s.Acronym)
               .HasColumnType("char(3)");
        }
    }
}
