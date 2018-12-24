using Lab.Data.Models;
using System;
using System.Linq;

namespace _14
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var project = context.Projects.Where(ep => ep.ProjectId == 2);
                var empProjects = context.EmployeesProjects.Where(ep => ep.ProjectId == 2);

                foreach (var ep in empProjects)
                {
                    context.EmployeesProjects.Remove(ep);
                }

                foreach (var p in project)
                {
                    context.Projects.Remove(p);
                }
                              
                context.SaveChanges();

                var projects = context.Projects.Take(10);

                foreach (var x in projects)
                {
                    Console.WriteLine($"{x.Name}");
                }
            }
        }
    }
}
