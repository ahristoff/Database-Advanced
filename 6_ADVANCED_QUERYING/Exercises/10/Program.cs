using BookShop.Data;
using System;
using System.Linq;

namespace _10
{
    class Program
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {                    
                var lengthCheck = int.Parse(Console.ReadLine());

                Console.WriteLine(CountBooks(db, lengthCheck));
            }
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                 .Where(c => c.Title.Length > lengthCheck);

            var res = books.Count();

            return res;
        }
    }
}
