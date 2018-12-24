using Employees.Services;
using System;
using System.Linq;

namespace Employees.App.Command
{
    public class SetAddressCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetAddressCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> <address> 
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            var address = String.Join(" ",args.Skip(1));
            var employeeName = employeeService.SetAddress(employeeId, address);

            return $"{employeeName}'s address was set to {address}";
        }
    }
}
