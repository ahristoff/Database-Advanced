using Employees.Services;

namespace Employees.App.Command
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        //<employeeId> 
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            var employee = employeeService.ById(employeeId);

            return $"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}";
        }
    }
}
