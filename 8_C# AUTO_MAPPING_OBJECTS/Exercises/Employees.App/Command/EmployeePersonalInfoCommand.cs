using Employees.Services;
using System;

namespace Employees.App.Command
{
    class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        //<employeeId> 
        public string Execute(params string[] args)
        {
            int employId = int.Parse(args[0]);

            var employee = employeeService.PersonalById(employId);

            var birthday = "[no birthday specified]";

            if (employee.BirthDay != null)
            {
                birthday = employee.BirthDay.Value.ToString("dd-MM-yyyy");
            }

            var address = employee.Address ?? "[no address specified]";

            string result = $"ID: {employId} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}" + Environment.NewLine +
                            $"Birthday: {birthday}" + Environment.NewLine +
                            $"Address: {address}";

            return result;
        }
    }
}
