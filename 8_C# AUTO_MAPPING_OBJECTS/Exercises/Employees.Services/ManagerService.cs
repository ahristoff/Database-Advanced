using AutoMapper;
using Employees.Data;
using Employees.DtoModels;

namespace Employees.Services
{
    public class ManagerService
    {
        private readonly EmployeesContext context;

        public ManagerService(EmployeesContext context)
        {
            this.context = context;
        }

        public string SetManager(int employeeId, int managerId)
        {
            
            var employee = context.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            var manager = context.Employees.Find(managerId);
            var managerDto = Mapper.Map<ManagerDto>(manager);

            managerDto.EmployeesDtos.Add(employeeDto);

            context.SaveChanges();

            return $"{employeeDto.FirstName} {employeeDto.LastName} was set to manager {managerDto.FirstName} {manager.LastName}";
        }

        public ManagerDto ManagerById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var managerDto = Mapper.Map<ManagerDto>(employee);

            return managerDto;
        }
    }
}
