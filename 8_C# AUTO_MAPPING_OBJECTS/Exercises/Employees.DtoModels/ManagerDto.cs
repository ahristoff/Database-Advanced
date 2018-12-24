using System.Collections.Generic;

namespace Employees.DtoModels
{
    public class ManagerDto
    {       
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> EmployeesDtos { get; set; } = new List<EmployeeDto>();

        public int CountEmployees => EmployeesDtos.Count;
    }
}
