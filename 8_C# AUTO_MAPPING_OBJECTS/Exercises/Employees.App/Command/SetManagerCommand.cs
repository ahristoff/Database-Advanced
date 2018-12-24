using Employees.DtoModels;
using Employees.Services;

namespace Employees.App.Command
{
    public class SetManagerCommand: ICommand
    {
        //<employeeId> <managerId> 

        private readonly EmployeeService employeeService;
        private readonly ManagerService managerService;

        public SetManagerCommand(EmployeeService employeeService, ManagerService managerService)
        {
            this.employeeService = employeeService;
            this.managerService = managerService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            var managerDto = new ManagerDto();

            managerService.SetManager(employeeId, managerId);

            var result = managerService.SetManager(employeeId, managerId);

            return result;
        }
    }
}
