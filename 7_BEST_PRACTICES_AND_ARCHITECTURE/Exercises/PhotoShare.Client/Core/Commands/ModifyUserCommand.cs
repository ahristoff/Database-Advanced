﻿namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string username = data[1];
            string property = data[2].ToLower();
            string newValue = data[3];

            using (var context = new PhotoShareContext())
            {
                var user = context.Users.Where(u => u.Username == username).FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                switch (property)
                {
                    case "password":
                        if (!newValue.Any(c => Char.IsLower(c)) || !newValue.Any(c => Char.IsDigit(c)))
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine +
                                "Invallid Password!");
                        }

                        user.Password = newValue;
                        break;

                   case "borntown":

                        var borntown = context.Towns.Where(t => t.Name == newValue).FirstOrDefault();
                        if (borntown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine +
                               $"Town {newValue} not found");
                        }
                        user.BornTown = borntown;
                        break;

                    case "currenttown":
                        var currenttown = context.Towns.Where(t => t.Name == newValue).FirstOrDefault();
                        if (currenttown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine +
                                $"Town {newValue} not found");
                        }
                        user.CurrentTown = currenttown;
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");                    
                }

                context.SaveChanges();

                return $"User {username} {property} is {newValue}.";
            }
        }
    }
}
