﻿namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;

    public class DeleteUser
    {
        // DeleteUser <username>
        public static string Execute(string[] data)
        {
            string username = data[1];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with {username} was not found!");
                }

                // TODO: Delete User by username (only mark him as inactive)
                if (user.IsDeleted == true)
                {
                    throw new InvalidOperationException($"User {username} is already deleted!");
                }

                user.IsDeleted = true;

                context.SaveChanges();

                return $"User {username} was deleted from the database!";
            }
        }
    }
}
