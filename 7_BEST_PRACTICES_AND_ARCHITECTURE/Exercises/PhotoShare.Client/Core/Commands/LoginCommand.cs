using PhotoShare.Data;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        public static string Execute(params string[] arguments)
        {
            var username = arguments[1];
            var password = arguments[2];

            using (var db = new PhotoShareContext())
            {
                if (Session.User != null)
                {
                    throw new InvalidOperationException("user is already logged in");
                }

                var user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user == null || user.IsDeleted == true)
                {
                    return "Invalide username or pasword";
                }

                Session.User = user;

                return $"{user.Username} logged in successfully";
            }           
        }
    }
}
