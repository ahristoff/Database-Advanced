using System;
using BookShop.Data;
using System.Linq;
using BookShop.Models;

namespace _01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new BookShopContext())
            {
                var command = Console.ReadLine();
                Console.WriteLine(GetBooksByAgeRestriction(db, command));
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int enumValue = -1;

            switch (command.ToLower())
            {
                case "minor":
                    enumValue = 0;
                    break;
                case "teen":
                    enumValue = 1;
                    break;
                case "adult":
                    enumValue = 2;
                    break;
            }

            var books = context.Books
                .Where(d => d.AgeRestriction == (AgeRestriction)enumValue)
                //.Where(c => (int)c.AgeRestriction == enumValue)
                .OrderBy(e => e.Title)
                .Select(b => b.Title)
                .ToList();

            string result = String.Join(Environment.NewLine, books);

            return result;
        }
    }
}
