using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Models
{
    public class User: BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmation { get; set; } = false;
        public bool LockoutEnable { get; set; } = false;
        public int LockoutCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public bool IsNewUser { get; set; } = true;
        [Column(TypeName = "timestamp without time zone"), DisplayFormat(DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime LastPasswordChangeDate { get; set; } = DateTime.Now;
        [Column(TypeName = "timestamp without time zone"), DisplayFormat(DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime LastLoginDate { get; set; } = DateTime.Now;
        [Column(TypeName = "timestamp without time zone"), DisplayFormat(DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime DeletedDate { get; set; } = DateTime.Now;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];   
        public Employee? Employee { get; set; }
        public RoleClass Role { get; set; } = RoleClass.Common;

    }
}