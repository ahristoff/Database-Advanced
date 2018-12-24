using Newtonsoft.Json;
using ProductsShop.Data;
using System.IO;
using System.Linq;

namespace JSON_2._1_Query_and_Export_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 500m && p.Price <= 1000m)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    }).ToArray();

                var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText("PricesInrange.json", jsonString);
            }
        }
    }
}
