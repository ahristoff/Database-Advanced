using BookShop.Data;
using System;
using System.Linq;

namespace _08
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var input = Console.ReadLine();

                Console.WriteLine(GetBookTitlesContaining(db, input));
            }
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            //1
            // string pattern = $"^*{input.ToLower()}.*$";

            // var authors = context.Books
            //.Where(a => Regex.Match(a.Title.ToLower(), pattern).Success)
            //.OrderBy(f => f.Title)
            //.Select(e => new
            //{
            //    e.Title
            //})
            //.ToList();

            //--------------------------------------------------------------------------------

            //2
            var books = context.Books
                .Where(a => a.Title.ToString().ToLower().IndexOf($"{input}".ToLower()) > -1)
                //.Where(e => e.Title.ToString().ToLower().Contains($"{input}".ToLower()))
                .OrderBy(f => f.Title)
                .Select(e => new
                {
                    e.Title,
                })
                .ToList();

            string result = String.Join(Environment.NewLine, books.Select(x => $"{x.Title}"));

            return result;
        }
    }
}
