using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Dtos.DepartmentDto;
using hitachiv1.Dtos.UserDto;

namespace hitachiv1.Dtos.EmployeeDto
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public GetUserDto? User { get; set; }
        public GetDepartmentDto? Department { get; set; }
    }
}