using BookShop.Data;
using System;
using System.Linq;

namespace _12
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine(GetTotalProfitByCategory(db));
            }
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var authors = context.Categories
                .Select(a => new
                {
                    a.Name,
                    prices = a.CategoryBooks.Select(b => b.Book.Price * b.Book.Copies).Sum()
                })
                .OrderByDescending(c => c.prices).ThenBy(c => c.Name)
                .ToList();

            string result = String.Join(Environment.NewLine, authors.Select(x => $"{x.Name} ${x.prices:f2}"));

            return result;
        }
    }
}
