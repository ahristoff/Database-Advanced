using Lab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _08
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                //1
                var adress = context.Addresses
                     .Include(a => a.Employees)
                     .Include(t => t.Town)
                     .OrderByDescending(e => e.Employees.Count)
                     .ThenBy(t => t.Town.Name)
                     .ThenBy(a => a.AddressText)
                     .Take(10);

                foreach (var x in adress)
                {
                    Console.WriteLine($"{x.AddressText}, {x.Town.Name} - {x.Employees.Count} employees");
                }

                //--------------------------------------------------------------------------

                //2 

                //var adr = context.Addresses
                //        .Select(e => new
                //         {
                //             AddressText = e.AddressText,
                //             Town = e.Town,
                //             Emp = e.Employees.Count

                //         })
                //     .OrderByDescending(e => e.Emp)
                //             .ThenBy(t => t.Town.Name)
                //             .ThenBy(a => a.AddressText)
                //             .Take(10);
                //foreach (var x in adr)
                //{
                //    Console.WriteLine($"{x.AddressText}, {x.Town.Name} - {x.Emp} employees");
                //}
            }
        }
    }
}
