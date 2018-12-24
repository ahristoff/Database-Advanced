using Employees.Services;
using System.Text;

namespace Employees.App.Command
{
    class ManagerInfoCommand: ICommand
    {
        //<employeeId>

        private readonly ManagerService managerService;

        public ManagerInfoCommand(ManagerService    managerService)
        {
            this.managerService = managerService;
        }

        public string Execute(params string[] args)
        {
            int managerId = int.Parse(args[0]);
            var manager = managerService.ManagerById(managerId);

            var sb = new StringBuilder();

            sb.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.CountEmployees}");

            foreach (var x in manager.EmployeesDtos)
            {
                sb.AppendLine($"- {x.FirstName} {x.LastName} - {x.Salary}");
            }

            return sb.ToString().Trim();
        }
    }
}
