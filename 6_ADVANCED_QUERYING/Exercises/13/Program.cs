using BookShop.Data;
using System;
using System.Linq;
using System.Text;

namespace _13
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine(GetMostRecentBooks(db));
            }
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var category = context.Categories
              .OrderBy(c => c.Name)
              .Select(a => new
              {
                  a.Name,
                  books = a.CategoryBooks.Select(b => b.Book)
                      .OrderByDescending(b => b.ReleaseDate).Take(3)
              })
              .ToList();

            var sb = new StringBuilder();

            foreach (var x in category)
            {
                sb.AppendLine($"--{x.Name}");
                foreach (var y in x.books)
                {
                    string date = null;
                    if (y.ReleaseDate == null)
                    {
                        date = "No Releasedaste";
                    }
                    else
                    {
                        date = y.ReleaseDate.Value.Year.ToString();
                    }
                    //Console.WriteLine($"{y.Title} ({Convert.ToDateTime(y.ReleaseDate).ToString("yyyy")})");
                    sb.AppendLine($"{y.Title} ({date})");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
