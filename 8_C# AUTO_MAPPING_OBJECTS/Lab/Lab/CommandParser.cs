using Forum.App.Commands.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Forum.App
{
    public class CommandParser
    {
        public static ICommand ParseCommand(IServiceProvider serviceProvider, string CommandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
            .ToArray();

            var commandType = commandTypes.SingleOrDefault(t => t.Name == $"{CommandName}Command");

            if (commandType ==null)
            {
                throw new InvalidOperationException("InvalidCommand");
            }

            var constructor = commandType.GetConstructors().First();

            var constructorParameters = constructor
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(p => serviceProvider.GetService(p))
                .ToArray();

            var command = (ICommand)constructor.Invoke(services);

            return command;
        }
    }
}
