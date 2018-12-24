﻿using System;
using System.Collections.Generic;

namespace Lab.Data.Models
{
    public class Employee
    {
        public Employee()
        {
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int? AddressId { get; set; }

        public Address Address { get; set; }
        public Department Department { get; set; }
        public Employee Manager { get; set; }
       
        public ICollection<EmployeeProject> EmployeesProjects { get; set; } = new List<EmployeeProject>();
        
    }
}
