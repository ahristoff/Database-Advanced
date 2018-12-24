using BookShop.Data;
using System;
using System.Linq;

namespace _04
{
    class Program
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                var year = int.Parse(Console.ReadLine());

                Console.WriteLine(GetBooksNotRealeasedIn(db, year));
            }
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(e => e.ReleaseDate.Value.Year != year)
                //.Where(b => Convert.ToDateTime(b.ReleaseDate).ToString("yyyy") != year.ToString())
                .Select(c => new
                {
                    c.Title,
                    c.BookId
                })
                .OrderBy(b =>b.BookId);

            string result = String.Join(Environment.NewLine, books.Select(x => $"{x.Title}"));

            return result;
        }
    }
}

