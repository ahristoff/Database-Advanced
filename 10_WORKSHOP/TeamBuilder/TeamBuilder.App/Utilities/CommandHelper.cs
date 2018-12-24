using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.UserName == username);
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Invitations
                    .Any(u => u.Team.Name == teamName && u.InvitedUserId == user.Id && u.IsActive);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Teams
                    .Single(c => c.Name == teamName)
                    .UserTeams.Any(u => u.User.UserName == username );
            }
        }

        public static bool IsEventExisting(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Events
                    .Any(c => c.Name == teamName);
            }
        }

        public static bool IsUserCreatorOfEvent()
        {
            return true;
        }

        public static bool IsUserCreatorOfTeam()
        {
            return true;
        }

        
    }
}
