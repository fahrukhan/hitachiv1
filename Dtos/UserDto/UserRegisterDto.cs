using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Dtos.UserDto
{
    public class UserRegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleClass Role { get; set; } = RoleClass.Common;
    }
}