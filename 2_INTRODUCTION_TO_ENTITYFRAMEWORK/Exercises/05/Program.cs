using Lab.Data.Models;
using System;
using System.Linq;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var selectedEmployees = context.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        DepartmentName = e.Department.Name,
                        e.Salary
                    });

                foreach (var x in selectedEmployees)
                {
                    Console.WriteLine($"{x.FirstName} {x.LastName} from {x.DepartmentName} - ${x.Salary:f2}");
                }
            }
        }
    }
}
