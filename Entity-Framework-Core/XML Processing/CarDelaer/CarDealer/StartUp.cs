using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.DTO.Output;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var dbContext =   new CarDealerContext();

            //InitializeDatabase(dbContext);

            ////01. Import Suppliers
            //var inputXMLSuppliers = File.ReadAllText("./Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(dbContext, inputXMLSuppliers));

            //////02. Imports Parts
            //var inputXMLParts = File.ReadAllText("./Datasets/parts.xml");
            //Console.WriteLine(ImportParts(dbContext, inputXMLParts));

            //////03. Import Cars
            //var inputXMLCars = File.ReadAllText("./Datasets/cars.xml");
            //Console.WriteLine(ImportCars(dbContext, inputXMLCars));

            ////04. Import Customers
            //var inputXMLCustomers = File.ReadAllText("./Datasets/customers.xml");
            //Console.WriteLine(ImportCustomers(dbContext, inputXMLCustomers));

            ////05. Import Sales

            //var inputXMLSales = File.ReadAllText("./Datasets/sales.xml");
            //Console.WriteLine(ImportSales(dbContext, inputXMLSales));

            ////06. Export Cars With Distance
            //Console.WriteLine(GetCarsWithDistance(dbContext));

            //07. Export Cars From Make BMW
            //Console.WriteLine(GetCarsFromMakeBmw(dbContext));

            //08.Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(dbContext));

            //09.Export Cars With Their List Of Parts
            Console.WriteLine(GetCarsWithTheirListOfParts(dbContext));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            const string ROOT =  "Suppliers";

            var xmlSerializer  = new XmlSerializer(typeof(SupplierInputModel[]), new XmlRootAttribute(ROOT));

            var textRead =  new StringReader(inputXml);

            var values = xmlSerializer
                .Deserialize(textRead) as SupplierInputModel[];

            var suppliers = values
                .Select(s=> new Supplier
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter,

                }).ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {values.Count()}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer  = new XmlSerializer(typeof(PartsInputModel[]), new XmlRootAttribute("Parts"));
            var textRead =  new StringReader(inputXml);
            var values = xmlSerializer.Deserialize(textRead) as PartsInputModel[];

            //var validSuppliersId = context.Suppliers.Select(i=>i.Id).ToList();

            var parts = values.Select(p=>  new Part
            {
                Name = p.Name,
                Price = p.Price,
                Quantity=p.Quantity,
                SupplierId=p.SupplierId,

            })
             .Where(s=> context.Suppliers.Any(i=>i.Id == s.SupplierId))
             .ToList();

            //var parts =  new List<Part>();

            //foreach (var part in CurrParts)
            //{
            //    if (validSuppliersId.Contains(part.SupplierId))
            //    {
            //        parts.Add(part);
            //    }
            //}

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer  = new XmlSerializer(typeof(CarInputModel[]), new XmlRootAttribute("Cars"));
            var textRead =  new StringReader(inputXml);
            var values = xmlSerializer.Deserialize(textRead) as CarInputModel[];

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            //var partsId = context.Parts.Select(i=>i.Id).ToList();

            foreach (var currCar in values)
            {
                var car = new Car()
                {
                    Make = currCar.Make,
                    Model = currCar.Model,
                    TravelledDistance = currCar.TravelledDistance
                };

                var distinctPart = currCar
                    .Parts
                    .Where(pc => context.Parts.Any(x=>x.Id == pc.PartId))
                    .Select(pc=>pc.PartId)
                    .Distinct();

                foreach (var part in distinctPart)
                {
                    PartCar partcar = new PartCar
                    {
                        PartId = part,
                        Car = car,
                    };

                    partCars.Add(partcar);
                }

                cars.Add(car);
            }

            context.PartCars.AddRange(partCars);
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {context.Cars.Count()}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {

            XmlSerializer xmlSerializer =  new XmlSerializer(typeof (CustomerInputModel[]),  new XmlRootAttribute("Customers"));
            var textRead =  new StringReader(inputXml);

            var values = xmlSerializer.Deserialize(textRead) as CustomerInputModel[];

            var customers = values.Select(c=>new Customer
            {
                Name= c.Name,
                BirthDate= c.BirthDate,
                IsYoungDriver= c.IsYoungDriver,
            }).ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof (SalesInputModel[]), new XmlRootAttribute("Sales"));
            var values = xmlSerializer.Deserialize(new StringReader(inputXml)) as SalesInputModel[];

            var sales = values.Select(s=> new Sale
            {
                CarId = s.CarId,
                CustomerId = s.CustomerId,
                Discount = s.Discount,

            }).Where(c=>context.Cars.Any(i=>i.Id==c.CarId))
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }


        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(c=>c.TravelledDistance > 2_000_000)
                .Select(c=> new CarOutputModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(m=>m.Make)
                .ThenBy(m=>m.Model)
                .Take(10)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CarOutputModel[]), new XmlRootAttribute("cars"));

            var stringWriter = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            xmlSerializer.Serialize(stringWriter, cars, ns);

            var result = stringWriter.ToString();

            return result;
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var bmwCars = context
                .Cars
                .Where(c=>c.Make == "BMW")
                .OrderBy(m=>m.Model)
                .ThenByDescending(t=>t.TravelledDistance)
                 .Select(x => new BmwOutputModel
                 {
                     Id=x.Id,
                     Model=x.Model,
                     TravelledDistance=x.TravelledDistance
                 }).ToArray();

            var xmlSerializer = new XmlSerializer(typeof(BmwOutputModel[]), new XmlRootAttribute("cars"));
            var stringWriter = new StringWriter();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(stringWriter, bmwCars, namespaces);
            var result = stringWriter.ToString().TrimEnd();

            return result;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(s=>s.IsImporter == false)
                .Select(s=> new LocalSupplierOutputModel
                {
                    Id=  s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                }).ToArray();

            var xmlSerializer = new XmlSerializer(typeof(LocalSupplierOutputModel[]), new XmlRootAttribute("suppliers"));
            var stringWriter = new StringWriter();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            xmlSerializer.Serialize(stringWriter, suppliers, namespaces);

            var result = stringWriter.ToString().TrimEnd();

            return result;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            //Sort all cars by travelled distance(descending) then by model(ascending).
            //Select top 5 records.

            var carsWithParts = context
                .Cars
                .Select(c=> new CarWithPartsOutputModel
                {
                    Make= c.Make,
                    Model= c.Model,
                    TravelledDistance= c.TravelledDistance,
                    Parts = c.PartCars.Select(p => new PartDto
                    {
                        Name=p.Part.Name,
                        Price=p.Part.Price
                    })
                    .OrderByDescending(p=>p.Price)
                    .ToArray()
                })
                .OrderByDescending(x => x.TravelledDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CarWithPartsOutputModel[]), new XmlRootAttribute("cars"));
            var stringWriter = new StringWriter();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            xmlSerializer.Serialize(stringWriter, carsWithParts, namespaces);

            var result = stringWriter.ToString().TrimEnd();
            
            return result;
        }

        private static void InitializeDatabase(CarDealerContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            Console.WriteLine("Success");
        }
    }
}