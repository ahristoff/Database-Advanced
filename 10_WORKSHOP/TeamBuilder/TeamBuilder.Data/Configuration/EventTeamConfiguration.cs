using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class EventTeamConfiguration : IEntityTypeConfiguration<EventTeam>
    {
        public void Configure(EntityTypeBuilder<EventTeam> builder)
        {
            builder.ToTable("EventTeams");

            builder.HasKey(c => new { c.EventId, c.TeamId });

            builder.HasOne(e => e.Event)
                .WithMany(e => e.ParticipatingEventTeam)
                .HasForeignKey(et => et.EventId);

            builder.HasOne(e => e.Team)
                .WithMany(e => e.EventTeams)
                .HasForeignKey(et => et.EventId);
        }
    }
}
