using Lab.Data.Models;
using System;
using System.Linq;

namespace _10
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var dep = context.Departments
                   .Where(e => e.Employees.Count > 5)
                   .Select(c => new
                    {
                        c.Name,
                        manf = c.Manager.FirstName,
                        manl = c.Manager.LastName,
                        empl = c.Employees.Select(s => new
                        {
                            s.FirstName,
                            s.LastName,
                            s.JobTitle
                        })
                    });

                foreach (var x in dep)
                {
                    Console.WriteLine($"{x.Name} - {x.manf} {x.manl}");
                    foreach (var y in x.empl.OrderBy(e => e.FirstName).ThenBy(s => s.LastName))
                    {
                        Console.WriteLine($"{y.FirstName} {y.LastName} - {y.JobTitle}");
                    }
                    Console.WriteLine("----------");
                }
            }
        }
    }
}
