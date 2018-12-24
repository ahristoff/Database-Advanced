using Newtonsoft.Json;
using ProductsShop.Data;
using System;
using System.Linq;

namespace JSON_2._2_Successfully_Sold_Products
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductSellers.Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(c => new
                    {
                        c.FirstName,
                        c.LastName,
                        SoldProducts = c.ProductSellers.Select(v => new
                        {
                            v.Name,
                            v.Price,
                            v.Buyer.FirstName,
                            v.Buyer.LastName
                        })
                    }).ToArray();

                var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                Console.WriteLine(jsonString);             
            }
        }
    }
}
