using BookShop.Data;
using System;
using System.Linq;

namespace _05
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var input = Console.ReadLine();

                Console.WriteLine(GetBooksByCategory(db, input));
            }
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(new[] { "\t", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();  

            var books = context.Books
                .Where(b => b.BookCategories.All(c => categories.Contains(c.Category.Name.ToLower())))   
                .OrderBy(e => e.Title)
                .Select(b => new
                {
                    b.Title
                }).ToList();

            var result = string.Join(Environment.NewLine, books.Select(x => $"{x.Title}"));

            return result;
        }
    }
}
