using Lab.Data.Models;
using System;
using System.Linq;

namespace _11
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {               
                var prj = context.Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name);

                foreach (var x in prj)
                {
                    Console.WriteLine($"{x.Name}");
                    Console.WriteLine($"{x.Description}");
                    Console.WriteLine($"{x.StartDate:M/d/yyyy h:mm:ss tt}");
                  //Console.WriteLine($"{x.StartDate.ToString("M/d/yyyy h:mm:ss tt")}");
                }
            }
        }
    }
}
