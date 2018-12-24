using System;

namespace Employees.App.Command
{
    public class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("GoodBye!");
            Environment.Exit(0);

            return string.Empty; 
        }
    }
}
