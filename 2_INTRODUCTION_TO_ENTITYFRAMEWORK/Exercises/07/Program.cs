using Lab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _07
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                //1

                //var employee = context.Employees
                //    .Include(m => m.EmployeesProjects)
                //    .ThenInclude(p => p.Project)
                //    .Include(m => m.Manager)
                //    .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 &&
                //    p.Project.StartDate.Year < 2003))
                //    .Take(30);

                //foreach (var x in employee)
                //{
                //    Console.WriteLine($"{x.FirstName} {x.LastName} - Manager: {x.Manager.FirstName} {x.Manager.LastName}");

                //    foreach (var y in x.EmployeesProjects)
                //    {
                //        if (y.Project.EndDate == null)
                //        {
                //            Console.WriteLine($"--{y.Project.Name} - {y.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - not finished");
                //        }
                //        else
                //        {
                //            DateTime endDate = Convert.ToDateTime(y.Project.EndDate);
                //            Console.WriteLine($"--{y.Project.Name} - {y.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {endDate.ToString("M/d/yyyy h:mm:ss tt")}");
                //        }
                //    }
                //}
            
                //---------------------------------------------------------------------------------

                // 2

                var employee = context.Employees                                                                         
                        .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 &&
                        p.Project.StartDate.Year < 2003))
                        .Select(m => new
                        {
                            m.FirstName,
                            m.LastName,
                            m.Manager,
                            prj = m.EmployeesProjects.Select(ep => ep.Project)
                        })
                        .Take(30);

                foreach (var x in employee)
                {
                    Console.WriteLine($"{x.FirstName} {x.LastName} - Manager: {x.Manager.FirstName} {x.Manager.LastName}");

                    foreach (var y in x.prj)
                    {
                        if (y.EndDate == null)
                        {
                            Console.WriteLine($"--{y.Name} - {y.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - not finished");
                        }
                        else
                        {
                            DateTime endDate = Convert.ToDateTime(y.EndDate);
                            Console.WriteLine($"--{y.Name} - {y.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {endDate.ToString("M/d/yyyy h:mm:ss tt")}");
                        }
                    }
                }
            }
        }
    }
}
