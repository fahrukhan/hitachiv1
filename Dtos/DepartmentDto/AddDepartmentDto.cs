using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Dtos.DepartmentDto
{
    public class AddDepartmentDto
    {
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }
}