using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Models
{
    public class Department: BaseEntity
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }
}