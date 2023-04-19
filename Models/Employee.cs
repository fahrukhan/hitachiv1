using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Dtos.UserDto;

namespace hitachiv1.Models
{
    public class Employee: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int UserId {get; set;}
        public User? User { get; set; }

        public int DepartmentId {get; set; }
        public Department? Department { get; set; }
    }
}