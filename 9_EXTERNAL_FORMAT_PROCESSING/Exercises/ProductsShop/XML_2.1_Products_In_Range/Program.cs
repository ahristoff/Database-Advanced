using ProductsShop.Data;
using System;
using System.Linq;
using System.Xml.Linq;

namespace XML_2._1_Products_In_Range
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Where(c => c.Buyer != null && c.Price >= 1000 && c.Price <= 2000)
                    .OrderBy(c => c.Price)
                    .Select(d => new
                    {
                        name = d.Name,
                        price = d.Price,
                        buyer = $"{d.Buyer.FirstName} {d.Buyer.LastName}"

                    }).ToArray();

                var doc = new XDocument();
                doc.Add(new XElement("products"));

                foreach (var x in products)
                {
                    var element = new XElement("product",
                        new XAttribute("name", x.name),
                        new XAttribute("price", x.price),
                        new XAttribute("buyer", x.buyer));

                    doc.Root.Add(element);
                }

                string xmlString = doc.ToString();

                Console.WriteLine(xmlString);
            }
        }
    }
}
