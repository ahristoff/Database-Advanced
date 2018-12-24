using Forum.Services.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Forum.App
{
    public class Engine
    {

        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run() 
        {
            var databaseInitializerService = serviceProvider.GetService<IDatabaseInitializerService>();
            databaseInitializerService.Initializedatabase();

            while (true)
            {
                Console.Write("Enter command: ");
                var input = Console.ReadLine();

                var commandTokens = input.Split(' ');

                var commandName = commandTokens.First();

                var commandArgs = commandTokens.Skip(1).ToArray();

                try
                {
                    var command = CommandParser.ParseCommand(serviceProvider, commandName);

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
