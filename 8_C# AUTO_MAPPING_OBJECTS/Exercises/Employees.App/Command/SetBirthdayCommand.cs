using Employees.Services;
using System;

namespace Employees.App.Command
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetBirthdayCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //SetBirthday <employeeId> <date: "dd-MM-yyyy">
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            var date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);
            var employeeName = employeeService.SetBirthDay(employeeId, date);

            return $"{employeeName}'s birthday was set to {args[1]}";
        }
    }
}
