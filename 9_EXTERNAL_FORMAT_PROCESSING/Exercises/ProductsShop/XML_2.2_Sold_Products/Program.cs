using ProductsShop.Data;
using System;
using System.Linq;
using System.Xml.Linq;

namespace XML_2._2_Sold_Products
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(c => c.ProductSellers.Count > 0)
                    .Select(s => new
                    {
                        firstname = s.FirstName,
                        lastname = s.LastName,
                        productt = s.ProductSellers.Select(d => new
                        {
                            namepr = d.Name,
                            pricepr = d.Price
                        })
                    }).ToArray();

                var doc = new XDocument();
                doc.Add(new XElement("users"));

                foreach (var x in users)
                {
                    var element = new XElement("user",
                            new XAttribute("last-name", x.lastname),
                            new XElement("sold-products",
                            x.productt.Select(y => new XElement("product",
                            new XElement("name", y.namepr), new XElement("price", y.pricepr)))));

                    if (x.firstname != null)
                    {
                        element.Add(new XAttribute("first-name", x.firstname));
                    }

                    doc.Root.Add(element);
                }

                string xmlString = doc.ToString();

                Console.WriteLine(xmlString);
            }
        }
    }
}
