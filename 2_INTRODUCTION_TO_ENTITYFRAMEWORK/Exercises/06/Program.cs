using Lab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _06
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
               // 1

                var adress = new Address()
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                context.Addresses.Add(adress);
                context.SaveChanges();

                var certainEmployee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
                certainEmployee.Address = adress;
                context.SaveChanges();

                var adrs = context.Addresses.OrderByDescending(e => e.AddressId)
                    .Take(10)
                    .Include(a => a.Employees)
                    .ToList();

                foreach (var x in adrs)
                {
                    Console.WriteLine(x.AddressText);
                }
            }
            //-----------------------------------------------------------------------------------
            ////2
            //    var adr = new Address()
            //    {
            //        AddressText = "Vitoshka 15",
            //        TownId = 4
            //    };
            //    var emp = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            //    emp.Address = adr;
            //    context.SaveChanges();
            //    var adrs = context.Employees.OrderByDescending(e => e.AddressId).Take(10).ToList();

            //    foreach (var x in adrs)
            //    {
            //        Console.WriteLine(x.Address.AddressText);
            //    }
            //}
        }
    }
}
