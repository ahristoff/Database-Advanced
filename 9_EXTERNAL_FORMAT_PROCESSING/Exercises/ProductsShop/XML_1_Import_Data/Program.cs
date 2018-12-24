using ProductsShop.Data;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace XML_1_Import_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ProductsShopContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine(ImportUsersFromXML());  

            Console.WriteLine(ImportCategoriesFromXML());   

            Console.WriteLine(ImportProductsFromXML());  
        }

        static string ImportUsersFromXML()  
        {

            var path = "../../../Files/users.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var users = new List<User>();

            foreach (var x in elements)
            {
                string firstName = x.Attribute("firstName")?.Value;

                string lastname = x.Attribute("lastName").Value;

                int? age = null;

                if (x.Attribute("age") != null)
                {
                    age = int.Parse(x.Attribute("age").Value);
                }

                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastname,
                    Age = age
                };

                users.Add(user);
            }

            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }

            return $"{users.Count} users were imported from file: {path}";
        } 

        static string ImportCategoriesFromXML()  
        {
            var path = "../../../Files/categories.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var categories = new List<Category>();

            foreach (var x in elements)
            {
                var category = new Category()
                {
                    Name = x.Element("name").Value
                };

                categories.Add(category);
            }

            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }

            return $"{categories.Count} categories were imported from file: {path}";
        }

        static string ImportProductsFromXML()   
        {
            var path = "../../../Files/products.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var catProducts = new List<CategoryProduct>();

            using (var db = new ProductsShopContext())
            {
                var usersId = db.Users.Select(u => u.Id).ToArray();
                var categoryIds = db.Categories.Select(u => u.Id).ToArray();

                Random rnd = new Random();

                foreach (var x in elements)
                {
                    string name = x.Element("name").Value;
                    decimal price = decimal.Parse(x.Element("price").Value);

                    int sellerindex = rnd.Next(0, usersId.Length);
                    int sellerId = usersId[sellerindex];
                    int? buyerId = sellerId;//

                    while (buyerId == sellerId)//
                    {
                        int buyerIndex = rnd.Next(0, usersId.Length);

                        buyerId = usersId[buyerIndex];
                    }

                    if (buyerId - sellerId < 5 && buyerId - sellerId > 0)//
                    {
                        buyerId = null;
                    }

                    var product = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,
                        BuyerId = buyerId//
                    };

                    int categoryIndex = rnd.Next(0, categoryIds.Length);
                    int categoryId = categoryIds[categoryIndex];

                    var catProduct = new CategoryProduct()
                    {
                        Product = product,
                        CategoryId = categoryId
                    };

                    catProducts.Add(catProduct);
                }

                db.AddRange(catProducts);
                db.SaveChanges();
            }

            return $"{catProducts.Count} products were imported from file :{path}";
        }
    }
}
