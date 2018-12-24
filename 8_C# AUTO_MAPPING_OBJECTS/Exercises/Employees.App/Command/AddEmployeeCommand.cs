using Employees.DtoModels;
using Employees.Services;

namespace Employees.App.Command
{
    class AddEmployeeCommand : ICommand
    {
        //<firstName> <lastName> <salary> 
        private readonly EmployeeService employeeService;

        public AddEmployeeCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto(firstName, lastName, salary);
            employeeService.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} successfully added";
        }
    }
}
