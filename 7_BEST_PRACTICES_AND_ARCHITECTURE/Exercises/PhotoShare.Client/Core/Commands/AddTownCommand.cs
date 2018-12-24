﻿namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string townName = data[1];
            string country = data[2];

            using (PhotoShareContext context = new PhotoShareContext()) 
            {
                //var townCheck = context.Towns
                //    .AsNoTracking()    
                //    .Where(t => t.Name == townName)
                //    .FirstOrDefault();

                if (context.Towns.Any(t => t.Name == townName))
                {
                    throw new ArgumentException("Town already exists");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return townName + " was added to database!";
            }
        }
    }
}
