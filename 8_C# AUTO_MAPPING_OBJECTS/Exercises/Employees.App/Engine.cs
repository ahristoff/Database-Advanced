using Employees.Data;
using System;
using System.Linq;

namespace Employees.App
{
    internal class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Run()
        {
            using (var context = new EmployeesContext())
            {
                //context.Database.Migrate();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                while (true)
                {
                    Console.Write("Enter command: ");

                    string input = Console.ReadLine();

                    string[] commandTokens = input.Split(' ');

                    string commandName = commandTokens[0];

                    string[] commandArgs = commandTokens.Skip(1).ToArray();

                    try
                    {
                        var command = CommandParser.Parse(serviceProvider, commandName);

                        var result = command.Execute(commandArgs);

                        Console.WriteLine(result);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }
    }
}
