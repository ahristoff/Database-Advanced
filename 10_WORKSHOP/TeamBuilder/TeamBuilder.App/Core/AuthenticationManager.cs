using System;
using TeamBuilder.Models;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core
{
    public class AuthenticationManager
    {
        private static User currentUser;
        
        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout(User user)
        {
            
            if (user == null)
            {
               Console.WriteLine("You are not loged in");
            }          
            currentUser = null;   
        }

        public static void Authorize()
        {
            if (currentUser == null)
            {               
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static bool isAuthenticated()
        {
            return true;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
