using ProductsShop.Data;
using System;
using System.Linq;
using System.Xml.Linq;

namespace XML_2._4_Users_and_Products
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(c => c.ProductSellers.Any())
                    .OrderByDescending(c => c.ProductSellers.Select(d => d.Id).Count())
                    .ThenBy(c => c.LastName)
                    .Select(a => new
                    {
                        a.FirstName,
                        a.LastName,
                        a.Age,
                        SoldProductsCount = a.ProductSellers.Count,
                        soldProducts = a.ProductSellers.Select(p => new
                        {
                            p.Name,
                            p.Price
                        })
                    });

                var doc = new XDocument();
                doc.Add(new XElement("users",
                    new XAttribute("count", db.Users
                    .Where(c => c.ProductSellers.Any()).Count())));

                foreach (var x in users)
                {
                    doc.Root.Add(new XElement("user",
                            new XAttribute("first-name", x.FirstName ?? ""),
                            new XAttribute("last-name", x.LastName),
                            new XAttribute("age", x.Age ?? 0),
                        new XElement("sold-products",
                            new XAttribute("count", x.SoldProductsCount),
                            x.soldProducts.Select(p => new XElement("product",
                                new XAttribute("name", p.Name), new XAttribute("price", p.Price)
                            )))));
                }
                string xmlString = doc.ToString();

                Console.WriteLine(xmlString);
            }
        }
    }
}
