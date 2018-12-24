using BookShop.Data;
using System;
using System.Linq;

namespace _07
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var input = Console.ReadLine();

                Console.WriteLine(GetAuthorNamesEndingIn(db, input));
            }
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input.ToLower()))
                .OrderBy(s => s.FirstName).ThenBy(s => s.LastName)
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                })
                .ToList();

            string result = String.Join(Environment.NewLine, authors.Select(x => $"{x.FirstName} {x.LastName}"));

            return result;
        }
    }
}
