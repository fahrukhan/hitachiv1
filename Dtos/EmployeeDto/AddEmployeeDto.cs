using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Dtos.EmployeeDto
{
    public class AddEmployeeDto
    {
        public string Name { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int UserId {get; set;}
        public int DepartmentId {get; set; }
    }
}