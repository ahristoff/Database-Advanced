using Lab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _09
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                //1
                var emp = context.EmployeesProjects
                    .Include(e => e.Employee)
                    .Include(p => p.Project)
                    .ToList();

                var curremp = emp.Where(e => e.EmployeeId == 147).ToList();

                var employee = context.Employees.Find(147);
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var x in curremp.OrderBy(n => n.Project.Name))
                {
                    Console.WriteLine($"{x.Project.Name}");
                }

                //---------------------------------------------------------------------------

                //// 2
                //var emp = context.Employees
                //         .Select(e => new
                //         {
                //             e.EmployeeId,
                //             e.FirstName,
                //             e.LastName,
                //             e.JobTitle,
                //             proj = e.EmployeesProjects.Select(ep => new
                //             {
                //                 pr = ep.Project.Name
                //             }).ToList()
                //         }).ToList();
                //var curremp = emp.FirstOrDefault(w => w.EmployeeId == 147);

                //Console.WriteLine($"{curremp.FirstName} {curremp.LastName} {curremp.JobTitle}");

                //foreach (var x in curremp.proj.OrderBy(n => n.pr))
                //{
                //    Console.WriteLine(x.pr);
                //}
            }
        }
    }
}
