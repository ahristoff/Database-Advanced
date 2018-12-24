using Employees.App.Command;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.App
{
    internal class CommandParser  // without switch -> with reflection
    {
        public static ICommand Parse(IServiceProvider serviceProvider, string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly(); 
            var commandTypes = assembly.GetTypes()
                .Where(g => g.GetInterfaces().Contains(typeof(ICommand)));

            var commandType = commandTypes.SingleOrDefault(d => d.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalide Command!");
            }

            var constructor = commandType.GetConstructors().First();

            var constructorParams = constructor 
                .GetParameters()  
                .Select(pi => pi.ParameterType)
                .ToArray(); 

            var constructorArgs = constructorParams
                .Select(serviceProvider.GetService)
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorArgs);

            return command;
        }
    }
}
