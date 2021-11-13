using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var dbContext = new ProductShopContext();

            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();

            string inputUsersJSON = File.ReadAllText("..\\..\\..\\Datasets\\users.json");
            string inputCategoriesJSON = File.ReadAllText("..\\..\\..\\Datasets\\categories.json");
            string inputProductsJSON = File.ReadAllText("..\\..\\..\\Datasets\\products.json");
            string inputCategoriesProductsJSON = File.ReadAllText("..\\..\\..\\Datasets\\categories-products.json");

            //Console.WriteLine(ImportUsers(dbContext, inputUsersJSON));
            //Console.WriteLine(ImportProducts(dbContext, inputProductsJSON));
            //Console.WriteLine(ImportCategories(dbContext, inputCategoriesJSON));
            //Console.WriteLine(ImportCategoryProducts(dbContext, inputCategoriesProductsJSON));
            //Console.WriteLine(GetProductsInRange(dbContext));
            //Console.WriteLine(GetSoldProducts(dbContext));
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {

            //collection of Users
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {

            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {

            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p=>p.Price >= 500 && p.Price <= 1000)
                .Select(p=> new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller =$"{p.Seller.FirstName} {p.Seller.LastName}",
                })
                .OrderBy(p=>p.Price)
                .ToList();

            var productsToJSON = JsonConvert.SerializeObject(products, Formatting.Indented);
            return productsToJSON;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {

            var usersWithSoldItem = context
                .Users
                .Where(u=>u.ProductsSold.Any(ps => ps.Buyer!= null))
                .Select(u=> new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                .Select(p=>new
                                {
                                    name=p.Name,
                                    price = p.Price,
                                    buyerFirstName= p.Buyer.FirstName,
                                    buyerLastName= p.Buyer.LastName
                                })

                })
                .OrderBy(u=>u.lastName)
                .ThenBy(u=>u.firstName)
                .ToList();

            var result = JsonConvert.SerializeObject(usersWithSoldItem, Formatting.Indented);
            return result;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context) {


            return "";
        }
    }
}