using Lab.Data.Models;
using System;
using System.Linq;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {            
            using (var context = new SoftUniDbContext())
            {
                var employee = context.Employees
                    .Where(e => e.Salary > 50000)
                    .OrderBy(e => e.FirstName)
                    .Select(e => e.FirstName)
                    .ToList();

                foreach (var x in employee)
                {
                    Console.WriteLine(x);
                }
            }        
        }
    }
}
