using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var dbContext =   new CarDealerContext();

            InitializeDatabase(dbContext);

            //01. Import Suppliers
            var inputXMLSuppliers = File.ReadAllText("./Datasets/suppliers.xml");
            Console.WriteLine(ImportSuppliers(dbContext, inputXMLSuppliers));

            //02. Imports Parts
            var inputXMLParts = File.ReadAllText("./Datasets/parts.xml");
            Console.WriteLine(ImportParts(dbContext, inputXMLParts));


            //03. Import Cars
            var inputXMLCars = File.ReadAllText("./Datasets/cars.xml");
            Console.WriteLine(ImportCars(dbContext, inputXMLCars));

            //var inputXMLCustomers = File.ReadAllText("./Datasets/customers.xml");
            //var inputXMLSales = File.ReadAllText("./Datasets/suppliers.xml");

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

            var validSuppliersId = context.Suppliers.Select(i=>i.Id).ToList();

            var CurrParts = values.Select(p=>  new Part
            {
                Name = p.Name,
                Price = p.Price,
                Quantity=p.Quantity,
                SupplierId=p.SupplierId,

            }).ToList();

            var parts =  new List<Part>();

            foreach (var part in CurrParts)
            {
                if (validSuppliersId.Contains(part.SupplierId))
                {
                    parts.Add(part);
                }
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer  = new XmlSerializer(typeof(CarInputModel[]), new XmlRootAttribute("Cars"));
            var textRead =  new StringReader(inputXml);
            var values = xmlSerializer.Deserialize(textRead) as CarInputModel[];
            ;
            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            var partsId = context.Parts.Select(i=>i.Id).ToList();

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

        private static void InitializeDatabase(CarDealerContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            Console.WriteLine("Success");
        }
    }
}