using BookShop.Data;
using System;
using System.Linq;

namespace _09
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var input = Console.ReadLine();

                Console.WriteLine(GetBooksByAuthor(db, input));
            }
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var authors = context.Books
               .Where(a => a.Author.LastName.StartsWith(input.ToLower()))
               .OrderBy(f => f.BookId)
               .Select(e => new
               {
                   e.Title,
                   e.Author.FirstName,
                   e.Author.LastName
               })
               .ToList();

            string result = String.Join(Environment.NewLine, authors.Select(x => $"{x.Title} ({x.FirstName} {x.LastName})"));

            return result;
        }
    }
}
