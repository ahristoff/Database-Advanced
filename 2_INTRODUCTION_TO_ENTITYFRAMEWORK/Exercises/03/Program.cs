using Lab.Data.Models;
using System;
using System.Linq;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var employee = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.MiddleName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeId)
                .ToList();
                foreach (var x in employee)
                {
                    Console.WriteLine($"{x.FirstName, -11} {x.MiddleName,-16} {x.LastName, -18} {x.JobTitle, -44} {x.Salary:f2}");
                }
            }
        }
    }
}
