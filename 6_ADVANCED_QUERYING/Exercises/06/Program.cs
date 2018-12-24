using BookShop.Data;
using System;
using System.Linq;

namespace _06
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var date = Console.ReadLine();

                Console.WriteLine(GetBooksReleasedBefore(db, date));
            }
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var books = context.Books             
                .Where(e => e.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", null))
                //.Where(e => e.ReleaseDate < Convert.ToDateTime(date))
                .Select(b => new  
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(e => e.ReleaseDate)
                .ToList();

            string result = String.Join(Environment.NewLine, books.Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}"));
            
            return result;
        }
    }
}
