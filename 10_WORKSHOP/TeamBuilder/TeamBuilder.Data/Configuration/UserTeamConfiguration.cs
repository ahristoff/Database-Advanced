using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(c => new { c.UserId, c.TeamId });

            builder.HasOne(e => e.User)
                .WithMany(e => e.UserTeams)
                .HasForeignKey(et => et.TeamId);

            builder.HasOne(e => e.Team)
                .WithMany(e => e.UserTeams)
                .HasForeignKey(et => et.TeamId);
        }
    }
}
