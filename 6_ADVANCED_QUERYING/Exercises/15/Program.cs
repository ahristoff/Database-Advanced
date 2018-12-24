using BookShop.Data;
using System;
using System.Linq;

namespace _15
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {           
                Console.WriteLine($"{RemoveBooks(db)} books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(e => e.Copies < 4200);

            int res = books.Count();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return res;
        }
    }
}
