using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class LogoutCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(0, inputArgs);
            AuthenticationManager.Authorize();
            User currentUser = AuthenticationManager.GetCurrentUser();
            AuthenticationManager.Logout(currentUser);
            return $"User {currentUser.UserName} successfully logged out!";
        }
    }
}
