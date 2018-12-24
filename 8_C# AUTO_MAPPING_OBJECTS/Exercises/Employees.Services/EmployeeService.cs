using Employees.Data;
using Employees.DtoModels;
using AutoMapper;
using System;
using Employees.Models;

namespace Employees.Services
{
    public class EmployeeService 
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId); 

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto; 
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);

            context.Employees.Add(employee);

            context.SaveChanges();
        }

        public string SetBirthDay(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            employee.BirthDay = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId); 

            var employeeDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeeDto;
        }
    }
}
