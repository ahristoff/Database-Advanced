using BookShop.Data;
using System;
using System.Linq;

namespace _03
{
    class Program
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine(GetBooksByPrice(db));
            }
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40m)
                .Select(c => new
                {
                    c.Price,
                    c.Title
                })
                .OrderByDescending(p => p.Price)
                .ToList();

            string result = String.Join(Environment.NewLine, books.Select(x => $"{x.Title} - ${x.Price:f2}"));

            return result;
        }
    }
}

