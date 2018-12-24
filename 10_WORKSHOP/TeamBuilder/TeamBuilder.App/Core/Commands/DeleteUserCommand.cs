using System.Linq;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class DeleteUserCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(0, inputArgs);
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();

            using (var context = new TeamBuilderContext())
            {
                var user = context.Users.FirstOrDefault(c => c.UserName == currentUser.UserName);
                    user.IsDeleted = true;
                context.SaveChanges();

                AuthenticationManager.Logout(currentUser);
            }

            return $"User {currentUser.UserName} was deleted successfully!";
        }       
    }
}
