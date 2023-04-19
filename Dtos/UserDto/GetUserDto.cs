using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Dtos.UserDto
{
    public class GetUserDto: BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmation { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public bool IsNewUser { get; set; } = true;
        public bool LockoutEnable { get; set; } = false;
        public int LockoutCount { get; set; } = 0;
        public DateTime LastPasswordChangeDate { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; } = DateTime.Now;
        public RoleClass Role { get; set; } = RoleClass.Common;

        //public GetEmployeeDto? Employee { get; set; }
    }
}