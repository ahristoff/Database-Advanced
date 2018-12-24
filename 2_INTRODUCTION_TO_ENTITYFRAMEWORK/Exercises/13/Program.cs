using Lab.Data.Models;
using System;
using System.Linq;

namespace _13
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var emp = context.Employees
                    .Where(e => e.FirstName.Substring(0, 2) == "Sa");

                foreach (var x in emp.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    Console.WriteLine($"{x.FirstName} {x.LastName} - {x.JobTitle} - (${x.Salary:f2})");
                }
            }
        }
    }
}
