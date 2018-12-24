using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserName).IsRequired();

            builder.HasIndex(c => c.UserName).IsUnique();

            builder.Property(c => c.Password).IsRequired();

            builder.Property(c => c.Password).HasMaxLength(30);


            builder.HasMany(u => u.CreatedTeams)
               .WithOne(t => t.Creator)
               .HasForeignKey(c => c.CreatorId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedEvents)
               .WithOne(t => t.Creator)
               .HasForeignKey(c => c.CreatorId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReceivedInvitations)
               .WithOne(t => t.InvitedUser)
               .HasForeignKey(c => c.InvitedUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
