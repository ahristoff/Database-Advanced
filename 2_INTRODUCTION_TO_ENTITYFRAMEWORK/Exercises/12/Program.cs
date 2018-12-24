using Lab.Data.Models;
using System;
using System.Linq;

namespace _12
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var emp = context.Employees
                    .Where(e => e.Department.Name == "Engineering" ||
                    e.Department.Name == "Tool Design" ||
                    e.Department.Name == "Marketing" ||
                    e.Department.Name == "Information Services");

                foreach (var x in emp.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    x.Salary = 1.12M * x.Salary;

                    Console.WriteLine($"{x.FirstName} {x.LastName} (${x.Salary:f2})");
                }

                context.SaveChanges();
            }
        }
    }
}
