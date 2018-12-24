using BookShop.Data;
using System;
using System.Linq;

namespace _11
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine(CountCopiesByAuthor(db));
            }
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(c => c.copies)
                .ToList();

            //var builder = new StringBuilder();

            //foreach (var x in authors)
            //{
            //    builder.AppendLine($"{x.Name} - {x.copies}");
            //}
            //return builder.ToString().Trim();

            string result = String.Join(Environment.NewLine, authors.Select(x => $"{x.Name} - {x.copies}"));

            return result;
        }
    }
}
