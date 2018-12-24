using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand
    {
        public static string Execute(string[] data)
        {
            string username = data[1];

            if (Session.User == null)
            {
                return "You are not loged in";
            }

            Session.User = null;

            return $"User {username} successfully logged out!";
        }
    }
}
