using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductsShop.Data;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JSON_1_Import_data
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ProductsShopContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();


            Console.WriteLine(ImportUsersFromJson());  

            Console.WriteLine(ImportCategoriesFromJson()); 

            Console.WriteLine(ImportProductsFromJson()); 

            SetCategories();
        }

        static string ImportUsersFromJson()    
        {
            string path = "../../../Files/users.json";

            User[] users = ImportJson<User>(path);           

            //var user = File.ReadAllText(path);
            //User[] users = JsonConvert.DeserializeObject<User[]>(user);

            using (var context = new ProductsShopContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            return $"{users.Length} users were imported from file: {path}";
        }

        static string ImportCategoriesFromJson()  
        {
            string path = "../../../Files/categories.json";

            Category[] categories = ImportJson<Category>(path);

            using (var context = new ProductsShopContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            return $"{categories.Length} categories were imported from file: {path}";
        }

        static string ImportProductsFromJson()  
        {
            string path = $"../../../Files/products.json";

            Random rnd = new Random();

            Product[] products = ImportJson<Product>(path);

            using (var context = new ProductsShopContext())
            {
                int[] usersId = context.Users.Select(u => u.Id).ToArray(); 
                                                                           
                foreach (var x in products)  
                {
                    int index = rnd.Next(0, usersId.Length); 
                    int sellerId = usersId[index];  
                    int? buyerId = sellerId;

                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, usersId.Length);

                        buyerId = usersId[buyerIndex];
                    }

                    if (buyerId - sellerId < 5 && buyerId - sellerId > 0) 
                    {
                        buyerId = null;
                    }

                    x.SellerId = sellerId;
                    x.BuyerId = buyerId;
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            return $"{products.Length} products were imported from file: {path}";
        }

        static void SetCategories() //1 Randomly generate categories for each product from the existing categories - on each product it gets 3 categories
        {
            using (var context = new ProductsShopContext())
            {
                var productsId = context.Products.AsNoTracking().Select(p => p.Id).ToArray();
               
                var categoriesId = context.Categories.AsNoTracking().Select(p => p.Id).ToArray();
                
                Random rnd = new Random();

                var categoryProducts = new List<CategoryProduct>();

                foreach (var x in productsId)  
                {

                    for (int i = 0; i < 3; i++)
                    {

                        int index = rnd.Next(0, categoriesId.Length);

                        while (categoryProducts.Any(s => s.CategoryId == categoriesId[index] &&
                         s.ProductId == x))
                        {
                            index = rnd.Next(0, categoriesId.Length);
                        }

                        var catPr = new CategoryProduct()
                        {
                            ProductId = x,
                            CategoryId = categoriesId[index]
                        };

                        categoryProducts.Add(catPr);
                    }
                }

                context.CategoryProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }
        }

        static T[] ImportJson<T>(string path)  
        {
            string jsonString = File.ReadAllText(path); 

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);  

            return objects;
        }
    }
}
