using Newtonsoft.Json;
using ProductsShop.Data;
using System;
using System.Linq;

namespace JSON_2._4_Users_and_Products
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var soldedUsers = context.Users
                    .Where(g => g.ProductSellers.Any())
                    .OrderByDescending(o => o.ProductSellers.Count())
                    .ThenBy(o => o.LastName)
                    .Select(a => new
                    {
                        firstName = a.FirstName,
                        lastName = a.LastName,
                        age = a.Age,
                        soldProducts = a.ProductSellers.Select(p => new
                        {
                            count = p.CategoryProducts.Select(d => d.Product).Count(),
                            products = p.CategoryProducts.Select(d => new
                            {
                                name = d.Product.Name,
                                price = d.Product.Price
                            })
                        })
                    })
                    .ToArray();

                var resultObj = new
                {
                    usersCount = soldedUsers.Length,
                    users = soldedUsers
                };

                var jsonString = JsonConvert.SerializeObject(resultObj, Formatting.Indented);

                Console.WriteLine(jsonString);
            }
        }
    }
}
