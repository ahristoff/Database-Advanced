using BookShop.Data;
using System.Linq;

namespace _14
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                IncreasePrices(db);
            }
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(e => e.ReleaseDate.Value.Year < 2010).ToList();

            foreach (var x in books)
            {
                x.Price += 5m;
            }

            context.SaveChanges();
        }
    }
}
