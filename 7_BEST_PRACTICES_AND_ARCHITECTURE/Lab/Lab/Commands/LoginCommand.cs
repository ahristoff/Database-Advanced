﻿using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;

namespace Forum.App.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] arguments)
        {
            var username = arguments[0];
            var password = arguments[1];

            var user = userService.ByUsernameAndPassword(username, password);

            if (user == null)
            {
                return "Invalide username or pasword";
            }

            Session.User = user;
            
            return $"{user.Username} logged in successfully";
        }
    }
}
