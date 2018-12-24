using Newtonsoft.Json;
using ProductsShop.Data;
using System;
using System.Linq;

namespace JSON_2._3_Categories_By_Products_Count
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.Name)
                    .Select(s => new
                    {
                        category = s.Name,
                        productsCount = s.CategoryProducts.Select(p => p.Product).Count(),
                        averagePrice = s.CategoryProducts.Select(v => v.Product.Price).Average(),
                        totalRevenue = s.CategoryProducts.Select(a => a.Product.Price).Sum(),

                    })
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);

                Console.WriteLine(jsonString);
            }
        }
    }
}
