using ProductsShop.Data;
using System;
using System.Linq;
using System.Xml.Linq;

namespace XML_2._3_Categories_By_Products_Count
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .OrderByDescending(c => c.CategoryProducts.Select(p => p.Product).Count())
                    .Select(c => new
                    {
                        names = c.Name,
                        productsCount = c.CategoryProducts.Select(p => p.Product).Count(),
                        averagePrice = c.CategoryProducts.Select(v => v.Product.Price).Average(),
                        totalRevenue = c.CategoryProducts.Select(a => a.Product.Price).Sum(),
                    }).ToArray();

                var doc = new XDocument();
                doc.Add(new XElement("categories"));

                foreach (var x in categories)
                {
                    doc.Root.Add(new XElement("category",
                            new XAttribute("name", x.names),
                        new XElement("products-count", x.productsCount),
                        new XElement("average-price", x.averagePrice),
                        new XElement("total-revenue", x.totalRevenue)));
                }

                string xmlString = doc.ToString();

                Console.WriteLine(xmlString);
            }
        }
    }
}
