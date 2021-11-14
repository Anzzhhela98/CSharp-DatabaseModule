using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private const string DATASETS_DIRECTORY_PATH = "../../../Datasets";
        public static void Main(string[] args)
        {
            var dbContext =  new CarDealerContext();

            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();

            //01.Import Suppliers
            //var inputSuppliersJSON = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/suppliers.json");
            //Console.WriteLine(ImportSuppliers(dbContext, inputSuppliersJSON));

            //02.Import Parts
            //var inputPartsJSON = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/parts.json");
            //Console.WriteLine(ImportParts(dbContext, inputPartsJSON));

            //03.Import Cars
            //var inputCarsJSON = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/cars.json");
            //Console.WriteLine(ImportCars(dbContext, inputCarsJSON));

            //04.Import Customers
            //var inputCustomersJSON = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/customers.json");
            //Console.WriteLine(ImportCustomers(dbContext, inputCustomersJSON));

            //04.Import Customers
            //var inputSalesJSON = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/sales.json");
            //Console.WriteLine(ImportSales(dbContext, inputSalesJSON));

            //05.Export Ordered Customers
            //Console.WriteLine(GetOrderedCustomers(dbContext));

            //06.Export Cars From Make Toyota
            //Console.WriteLine(GetCarsFromMakeToyota(dbContext));

            //07.Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(dbContext));

            //08.Export Cars With Their List Of Parts
            //Console.WriteLine(GetCarsWithTheirListOfParts(dbContext));

            //09.Export Total Sales By Customer
            //Console.WriteLine(GetTotalSalesByCustomer(dbContext));

            //10.Export Sales With Applied Discount
            Console.WriteLine(GetSalesWithAppliedDiscount(dbContext));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {

            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var suppliersID = context.Suppliers.Select(s=>s.Id).ToArray();

            var parts = JsonConvert
                .DeserializeObject<Part[]>(inputJson)
                .Where(s=>suppliersID.Contains(s.SupplierId))
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert
                .DeserializeObject<Car[]>(inputJson);

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {

            var cuctomers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(cuctomers);
            context.SaveChanges();

            return $"Successfully imported {cuctomers.Count()}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {

            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {

            var customers = context
                .Customers
                .OrderBy(c =>c.BirthDate)
                .ThenBy(c=>c.IsYoungDriver)
                .Select(c => new
                {
                    Name =c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver,
                })
                .ToList();

            var result  = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return result;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {

            var cars = context
                .Cars
                .Where(c => c.Make  == "Toyota")
                .OrderBy(c=>c.Model)
                .ThenByDescending(c =>c.TravelledDistance)
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                 .ToList();

            var result =JsonConvert.SerializeObject(cars, Formatting.Indented) ;

            return result;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {

            var suppliers = context.
                Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id= s.Id,
                    Name= s.Name,
                    PartsCount = s.Parts.Count(),
                }).ToList();

            var result =JsonConvert.SerializeObject(suppliers,Formatting.Indented);
            return result;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    parts = c.PartCars
                        .Select(y=> new
                        {
                            Name =y.Part.Name,
                            Price = $"{y.Part.Price:f2}"
                        }),
                }).ToList();


            var result =JsonConvert.SerializeObject(cars,Formatting.Indented);
            return result;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Where(c => c.Sales.Any(s=>s.CustomerId == c.Id))
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney=c.Sales.Sum(y=>y.Car.PartCars.Sum(z=>z.Part.Price))
                })
                .OrderByDescending(x=>x.spentMoney)
                .ThenByDescending(x=>x.boughtCars)
                .ToList();

            var result =JsonConvert.SerializeObject(customers,Formatting.Indented);
            return result;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            //Get first 10 sales with information about the car, customer and price of the sale with and without discount. Export

            var sales = context
                .Sales
                    .Take(10)
                .Select(s=> new
                {
                    car = new { Make = s.Car.Make, Model = s.Car.Model, TravelledDistance = s.Car.TravelledDistance },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:f2}",
                    price = $"{(s.Car.Sales.Sum(y => y.Car.PartCars.Sum(z => z.Part.Price))):f2}",
                    priceWithDiscount =$"{(s.Car.Sales.Sum(y => y.Car.PartCars.Sum(z => z.Part.Price))-s.Car.Sales.Sum(y => y.Car.PartCars.Sum(z => z.Part.Price))*(s.Discount/100)) :f2}",
                });
            var result =JsonConvert.SerializeObject(sales,Formatting.Indented);
            return result;
        }
    }
}