using Cars.Data;
using Cars.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace Cars.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //choose another server or DB name
            //var ob = new DbContextOptionsBuilder();
            //ob.UseSqlServer(@"Server=(Name of Server);Database= CarsCars;Integrated Security=True;");
            //var context = new CarsDbContext(ob.Options);

            using (var context = new CarsDbContext())
            {
                RestartDb(context);

                var cars = context.Cars
                    .Select(s => new
                    {
                        s.Transmission,
                        ProductYear = s.ProductionYear,
                        s.Model,
                        s.Make,
                        s.Engine,
                        s.LicensePlate,
                        CarDealerShips = s.CarDealerShips.Select(c => new
                        {
                            c.DealerShip
                        }).ToArray()
                    }).ToArray();

                foreach (var x in cars)
                {
                    Console.WriteLine($"{x.Make.Name} {x.Model}");
                    Console.WriteLine($"Dealer: ");
                    foreach (var y in x.CarDealerShips)
                    {
                        Console.WriteLine($"--{y.DealerShip.Name}");
                    }

                    if (x.CarDealerShips.Count() == 0)
                    {
                        Console.WriteLine("--no dealer");
                    }

                    Console.WriteLine($"----Fuel: {x.Engine.FuelType}");
                    Console.WriteLine($"----Transmission: {x.Transmission}");
                    Console.WriteLine($"----Capacity: {x.Engine.Capacity}");
                    Console.WriteLine($"----HorsePower: {x.Engine.HorsePower}");
                    Console.WriteLine($"----ProductionYear: {x.ProductYear}");
                    if (x.LicensePlate != null)
                    {
                        Console.WriteLine($"------Plate Number: {x.LicensePlate.Number}");
                    }
                    else
                    {
                        Console.WriteLine($"------Plate Number: no number");
                    }
                    Console.WriteLine("==================================================");
                }


                //var cars = context.Cars
                //    .Include(c => c.CarDealerShips)
                //    .ThenInclude(c => c.DealerShip)
                //    .Include(c => c.Engine)
                //    .Include(c => c.Make)
                //    .Include(c => c.LicensePlate)
                //    .OrderBy(c => c.ProductionYear)
                //    .ToArray();

                //var pl = context.LicensePlates.ToArray();
                //cars[0].LicensePlate = pl[2];  
                //cars[2].LicensePlate = pl[1];

                //foreach (var x in cars)
                //{

                //    Console.WriteLine($"{x.Make.Name} {x.Model}");
                //    Console.WriteLine($"--Dealership: ");

                //    foreach (var z in x.CarDealerShips)
                //    {
                //        Console.WriteLine($"----{z.DealerShip.Name}");
                //    }
                //    Console.WriteLine($"--Fuel: {x.Engine.FuelType}");      
                //    Console.WriteLine($"--Transmission: {x.Transmission}");
                //    string y = x.LicensePlate != null ? x.LicensePlate.Number : "No plate";
                //    Console.WriteLine($"--Plate Number: {y}");
                //    Console.WriteLine("-------------------------");
                //}
            }
            Console.WriteLine();
        }

        private static void RestartDb(CarsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated(); //context.Database.Migrate() -> if Migration avalilable

            Seed(context);
        }

        private static void Seed(CarsDbContext context)
        {
            var makes = new[]
            {
               new Make{ Name = "Ford"},
               new Make{ Name = "Mercedes"},
               new Make{ Name = "Audi"},
               new Make{ Name = "Bmw"},
               new Make{ Name = "АЗЛК"},
               new Make{ Name = "Лада"},
               new Make{ Name = "Трабант"},
            };
            context.Makes.AddRange(makes);

            var engines = new[]
            {
                new Engine{ Capacity = 1.6, Cyllinders = 4, FuelType = FuelType.Petrol, HorsePower = 95 },

                new Engine{ Capacity = 3.0, Cyllinders = 8, FuelType = FuelType.Electric, HorsePower = 318 },

                new Engine{ Capacity = 1.2, Cyllinders = 3, FuelType = FuelType.Gas, HorsePower = 60 }
            };
            context.Engines.AddRange(engines);

            var licensePlates = new[]
            {
                 new LicensePlate{ Number = "CB1234AN"},
                 new LicensePlate{ Number = "CB4567BC"},
                 new LicensePlate{ Number = "BP9999AA"},
            };
            context.LicensePlates.AddRange(licensePlates);

            var cars = new[]
            {
                new Car
                {
                    Engine = engines[2],
                    Make = makes[6],
                    Doors = 4,
                    Model = "Кашон-P50",
                    ProductionYear = new DateTime(1958, 1, 1),
                    Transmission = Transmission.Manuel,
                    LicensePlate = licensePlates[1]
                },

                 new Car
                {
                    Engine = engines[1],
                    Make = makes[4],
                    Doors = 3,
                    Model = "Москвич-423",
                    ProductionYear = new DateTime(1954, 1, 1),
                    Transmission = Transmission.Automatic
                },

                  new Car
                {
                    Engine = engines[0],
                    Make = makes[0],
                    Doors = 4,
                    Model = "Escort",
                    ProductionYear = new DateTime(1955, 1, 1),
                    Transmission = Transmission.Automatic,
                    LicensePlate = licensePlates[2]
                }
            };
            context.Cars.AddRange(cars);

            var dealerships = new[]
            {
                new DealerShip{ Name = "SoftUni-Auto"},
                new DealerShip{ Name = "Fast and Furious Auto"}
            };
            context.DealerShips.AddRange(dealerships);

            var cardealerships = new[]
            {
                new CarDealerShip{ Car = cars[0], DealerShip = dealerships[0]},
                new CarDealerShip{ Car = cars[0], DealerShip = dealerships[1]},
                new CarDealerShip{ Car = cars[1], DealerShip = dealerships[1]},
                
            };
            context.CarDealerShips.AddRange(cardealerships);
            
            context.SaveChanges();
        }
    }
}
