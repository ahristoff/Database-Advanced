using BookShop.Data;
using BookShop.Models;
using System;
using System.Linq;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BookShopContext())
            {              
                Console.WriteLine(GetGoldenBooks(db));
            }
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 5000 && b.EditionType == (EditionType)2)
                //or (int)b.EdiyionType == 2
                //or b.EditionType == EditionType.Gold
                .Select(c => new                                                            
                {
                    c.Title,
                    c.BookId
                })
                .OrderBy(s => s.BookId);

            string result = String.Join(Environment.NewLine, books.Select(b => b.Title));

            return result;
        }
    }
}
